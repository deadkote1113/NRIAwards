using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using NRIAwards.BL.Base.Interface.Crud;
using NRIAwards.Common.Configuration;
using NRIAwards.Common.Configuration.Mail;
using NRIAwards.Common.Entity;
using NRIAwards.Common.Entity.Search;
using NRIAwards.DAL.Context;
using NRIAwards.DependencyInjection;
using NRIAwards.PL.Ui.Extensions.Middleware;
using NRIAwards.PL.Ui.Models;
using NRIAwards.PL.Ui.Models.FileManager;
using System.Security.Claims;

namespace NRIAwards.PL.Ui
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/Users/Login");
                options.AccessDeniedPath = new PathString("/Admin/Users/Login");
                options.ExpireTimeSpan = new TimeSpan(7, 0, 0, 0);
                options.Events.OnValidatePrincipal += async context =>
                {
                    if (!context.Principal.Identity.IsAuthenticated)
                    {
                        return;
                    }

                    ICrudUsersService service = context.HttpContext.RequestServices.GetRequiredService<ICrudUsersService>();
                    User user = await service.GetFirstAsync(new UsersSearchParams()
                    {
                        Login = context.Principal.Identity.Name
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
            services.AddAuthorization();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddOptions<FileManagerOptions>().Bind(Configuration.GetSection("FileManager")).Configure(options =>
            {
                options.OnFileTypeValidate = (fileName, contentType) =>
                {
                    return new[] { "exe", "dll", "bat" }.Aggregate(true, (result, extension) =>
                        result & !fileName.EndsWith(extension));
                };
            });

            //---------------------DI------------------------
            var connectionString = Configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<PostgresDbContext>((options) => options.UseNpgsql(connectionString));

            var di = new Di(ProjectConfiguration.Prod, services);
            di.Add();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            SharedConfiguration.UpdateSharedConfiguration(Configuration.GetConnectionString("DefaultConnectionString"),
                Configuration.GetSection("SmtpSettings").Get<SmtpConfiguration>());

            app.UseStatusCodePagesWithReExecute(context =>
            {
                var area = context.HttpContext.Request.RouteValues["area"];
                var statusCode = context.HttpContext.Response.StatusCode;
                return StringComparer.InvariantCulture.Compare(area, "Admin") == 0
                    ? $"/Admin/Error/{statusCode}"
                    : $"/Error/{statusCode}";
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
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
        }
    }
}
