using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMonitorLibrary.Interfaces;

namespace SystemMonitor.Services
{
    public class MonitoringOptions
    {
        public int IntervalSeconds { get; set; } = 5;
        public string PostEndpoint { get; set; } = "";
        public bool EnableHttpPlugin { get; set; } = true;
        public bool EnableFilePlugin { get; set; } = true;
        public string PluginsFolder { get; set; } = "plugins";
    }

    public class MonitoringService : IHostedService,IDisposable
    {
        private readonly ISystemMonitor _monitor;
        private readonly IEnumerable<IMonitorPlugin> _plugins;
        private readonly ILogger<MonitoringService> _logger;
        private readonly MonitoringOptions _options;
        private Timer _timer;
        public MonitoringService( ISystemMonitor monitor,IEnumerable<IMonitorPlugin> plugins, IOptions<MonitoringOptions> options, ILogger<MonitoringService> logger)
        {
            _monitor = monitor;
            _plugins = plugins;
            _logger = logger;
            _options = options.Value;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Monitoring service starting. Interval: {i}s", _options.IntervalSeconds);
            _timer = new Timer(async _ => await TickAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(Math.Max(1, _options.IntervalSeconds)));
            return Task.CompletedTask;
        }
        private async Task TickAsync()
        {
            try
            {
                var metrics = await _monitor.GetMetricsAsync();
                Console.WriteLine($"{metrics.TimestampUTC:O} CPU:{metrics.CpuPercent}% RAM:{metrics.RamUsagePercent} DISK: {metrics.DiskUsagePercent}%");
                
                var tasks = new List<Task>();
                foreach (var p in _plugins)
                {
                    try
                    {
                        tasks.Add(p.OnMetricsAsync(metrics));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Plugin {name} threw during OnMetricsAsync initiation", p.PlugInName);
                    }
                }
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in monitoring tick");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Monitoring service stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
