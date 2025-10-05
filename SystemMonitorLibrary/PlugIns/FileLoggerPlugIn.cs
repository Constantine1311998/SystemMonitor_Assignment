using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMonitorLibrary.DataModel;
using SystemMonitorLibrary.Interfaces;

namespace SystemMonitorLibrary.PlugIns
{
    public class FileLoggerPlugIn : IMonitorPlugin
    {
        public string PlugInName => "FileLogger";
        private readonly string _filePath;

        public FileLoggerPlugIn()
        {
            _filePath = Path.Combine(AppContext.BaseDirectory, "monitor.log");
        }
        public Task OnMetricsAsync(MonitorMetrics metrics)
        {
            var line = $"{metrics.TimestampUTC:O} CPU:{metrics.CpuPercent}% RAM:{metrics.RamUsagePercent} DISK: {metrics.DiskUsagePercent}%";
            try
            {
                File.AppendAllLines(_filePath, new[] { line });
            }
            catch
            {
                // plugin should not throw; swallow or optionally log via shared ILogger (not included here)
            }
            return Task.CompletedTask;
        }
    }
}
