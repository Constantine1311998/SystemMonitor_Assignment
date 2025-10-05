using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using SystemMonitor.Helper;
using SystemMonitor.Services;
using SystemMonitorLibrary.Interfaces;
using SystemMonitorLibrary.PlugIns;
using SystemMonitorLibrary.SystemMonitorFactory;

namespace SystemMonitor
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                             .ConfigureAppConfiguration((ctx, cfg) =>
                              {
                                  cfg.SetBasePath(Directory.GetCurrentDirectory());
                                  cfg.AddJsonFile("appsettings.json", optional: true);
                              })
                             .ConfigureServices((context, services) =>
                             {
                                 var conf = context.Configuration;
                                 var monOpts = conf.GetSection("Monitoring").Get<MonitoringOptions>() ?? new MonitoringOptions();
                                 services.Configure<MonitoringOptions>(conf.GetSection("Monitoring"));

                                 services.AddSingleton(typeof(ISystemMonitor), sp =>
                                       SystemMonitorFactory.Instance.CreateSystemMonitor(CommonHelpers.GetOperatingSystem()));
                                 services.AddSingleton<IMonitorPlugin, FileLoggerPlugIn>();
                                 services.AddSingleton<IMonitorPlugin>(sp => new HttpPosterPlugin(monOpts.PostEndpoint));
                                 services.AddHostedService<MonitoringService>();
                             })
                             .ConfigureLogging(logging =>
                             {
                                 logging.ClearProviders();
                                 logging.AddConsole();
                             })
                             .Build();

            await host.RunAsync();
        }
    }
}
