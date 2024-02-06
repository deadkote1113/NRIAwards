using NLog;
using NLog.Web;

namespace NRIAwards.PL.Ui;

public class Program
{
    public static void Main(string[] args)
    {
        var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        try
        {
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception exception)
        {
            logger.Error(exception, "App run was unsuccessful");
            throw;
        }
        finally
        {
            LogManager.Shutdown();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).UseNLog();
}

