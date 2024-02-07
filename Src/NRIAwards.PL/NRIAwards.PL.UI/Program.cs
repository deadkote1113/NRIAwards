using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using NLog;
using NLog.Web;
using NRIAwards.DAL.Context;
using NRIAwards.DependencyInjection;
using NRIAwards.PL.Ui.Models.FileManager;
using NRIAwards.PL.Ui.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using NRIAwards.Common.Configuration.Mail;
using NRIAwards.Common.Configuration;
using NRIAwards.PL.Ui.Extensions.Middleware;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
    {
        options.LoginPath = new PathString("/Admin/Users/Login");
        options.AccessDeniedPath = new PathString("/Admin/Users/Login");
        options.ExpireTimeSpan = new TimeSpan(7, 0, 0, 0);
        options.Events.OnValidatePrincipal += async context =>
        {
            if (context.Principal is not null &&
            context.Principal.Identity is not null &&
            context.Principal.Identity.IsAuthenticated == false)
            {
                return;
            }

            ICrudUsersService service = context.HttpContext.RequestServices.GetRequiredService<ICrudUsersService>();
            User user = await service.GetFirstAsync(new UsersSearchParams()
            {
                Login = context.Principal?.Identity?.Name
            });
            if (user == null || user.IsBlocked)
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return;
            }
            UserModel userModel = UserModel.FromEntity(user);
            context.ReplacePrincipal(new ClaimsPrincipal(new CustomUserIdentity(userModel)));
        };
    });
    builder.Services.AddAuthorization();
    builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

    //--------------------Configuration--------------
    builder.Services.AddOptions<FileManagerOptions>().Bind(builder.Configuration.GetSection("FileManager")).Configure(options =>
    {
        options.OnFileTypeValidate = (fileName, contentType) =>
        {
            return new[] { "exe", "dll", "bat" }.Aggregate(true, (result, extension) =>
                result & !fileName.EndsWith(extension));
        };
    });

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    var SmtpSettings = builder.Configuration.GetSection("SmtpSettings").Get<SmtpConfiguration>();

    SharedConfiguration configuration = new()
    {
        DbConnectionString = connectionString,
        SmtpConfiguration = SmtpSettings,
    };
    builder.Services.AddSingleton(configuration);

    //---------------------DI------------------------
    builder.Services.AddDbContext<PostgresDbContext>((options) => options.UseNpgsql(connectionString));

    var di = new Di(ProjectConfiguration.Prod, builder.Services);
    di.Add();

    var app = builder.Build();

    app.UseStatusCodePagesWithReExecute(context =>
    {
        var area = context.HttpContext.Request.RouteValues["area"];
        var statusCode = context.HttpContext.Response.StatusCode;
        return StringComparer.InvariantCulture.Compare(area, "Admin") == 0
            ? $"/Admin/Error/{statusCode}"
            : $"/Error/{statusCode}";
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Task.CompletedTask;
            });
        });
    }

    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = context =>
        {
            context.Context.Response.Headers.Append(HeaderNames.CacheControl, "public,max-age=31622400");
        }
    });

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "AdminDefaultRoute",
            pattern: "Admin/{controller=Home}/{action=Index}/{id?}",
            new { area = "Admin" });

        endpoints.MapControllerRoute(
            name: "AdminErrorRoute",
            pattern: "Admin/{controller=Error}/{code:int}",
            defaults: new { area = "Admin", action = "Index" });

        endpoints.MapControllerRoute(
            name: "PublicDefaultRoute",
            pattern: "{controller=Home}/{action=Index}/{id?}",
            new { area = "Public" });

        endpoints.MapControllerRoute(
            name: "PublicErrorRoute",
            pattern: "{controller=Error}/{code:int}",
            defaults: new { area = "Public", action = "Index" });
    });

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
