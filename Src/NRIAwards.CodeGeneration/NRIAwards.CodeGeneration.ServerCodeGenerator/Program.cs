using CodeGeneration.ServerCodeGenerator.Service;
using CodeGeneration.ServerCodeGenerator.Service.MergeUtility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using NRIAwards.Common.Configuration;
using NRIAwards.Common.Configuration.Mail;
using NRIAwards.DAL.Context;
using NRIAwards.DependencyInjection;

namespace CodeGeneration.ServerCodeGenerator;

internal class Program
{
	internal static void Main(string[] args)
	{
		var builder = CreateHostBuilder(args);
        var host = builder.Build();

		host.Run();
	}
	private static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureServices((hostContext, services) =>
				{
                    var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnectionString");
                    var SmtpSettings = hostContext.Configuration.GetSection("SmtpSettings").Get<SmtpConfiguration>();

                    SharedConfiguration sharedConfiguration = new()
                    {
                        SmtpConfiguration = SmtpSettings,
                    };
                    services.AddSingleton(sharedConfiguration);

                    services.AddScoped<CodeGenerator>();
					services.AddScoped<IMergeUtility, KDiff3MergeUtility>();
					
					services.AddDbContext<PostgresDbContext>((DbContextOptionsBuilder options) => options.UseNpgsql(connectionString));

					services.AddHostedService<CodeGenerationService>();
					//-------------DI--------------
					var dependencyInjectionAddre = new Di(ProjectConfiguration.Prod, services);
					dependencyInjectionAddre.Add();
				}
			).UseNLog();
}