using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using SystemMonitor.Helper;
using SystemMonitor.Services;
using SystemMonitorLibrary.Interfaces;
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

          services.AddSingleton(ISystemMonitor,SystemMonitorFactory.Instance.CreateSystemMonitor(CommonHelpers.GetOperatingSystem()));


          // register built-in plugins (you could instead load DLLs from plugins folder)
          services.AddSingleton<IMonitorPlugin, FileLoggerPlugin>();
          services.AddSingleton<IMonitorPlugin>(sp =>
              new HttpPosterPlugin(monOpts.PostEndpoint));

          // plugin folder scanning (optional): load external DLLs and register types
          string pluginsFolder = monOpts.PluginsFolder ?? "plugins";
          if (Directory.Exists(pluginsFolder))
          {
              foreach (var dll in Directory.GetFiles(pluginsFolder, "*.dll"))
              {
                  try
                  {
                      var asm = Assembly.LoadFrom(dll);
                      var types = asm.GetTypes().Where(t => typeof(IMonitorPlugin).IsAssignableFrom(t) && !t.IsAbstract);
                      foreach (var t in types)
                      {
                          services.AddSingleton(typeof(IMonitorPlugin), t);
                      }
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine($"Could not load plugin {dll}: {ex.Message}");
                  }
              }
          }

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
