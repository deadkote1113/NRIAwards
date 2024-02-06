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
using System.Configuration;

namespace CodeGeneration.ServerCodeGenerator;

internal class Program
{
	internal static void Main(string[] args)
	{
		var host = CreateHostBuilder(args).Build();
		var configuration = host.Services.GetService<IConfiguration>();

		SharedConfiguration.UpdateSharedConfiguration(configuration.GetConnectionString("DefaultConnectionString"),
				configuration.GetSection("SmtpSettings").Get<SmtpConfiguration>());
		
		host.Run();
	}
	private static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureServices((hostContext, services) =>
				{
					services.AddScoped<CodeGenerator>();
					services.AddScoped<IMergeUtility, KDiff3MergeUtility>();
					
					var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnectionString");
					services.AddDbContext<PostgresDbContext>((DbContextOptionsBuilder options) => options.UseNpgsql(connectionString));

					services.AddHostedService<CodeGenerationService>();
					//-------------DI--------------
					var dependencyInjectionAddre = new Di(ProjectConfiguration.Prod, services);
					dependencyInjectionAddre.Add();
				}
			).UseNLog();
}


