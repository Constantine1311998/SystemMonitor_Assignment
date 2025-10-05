using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMonitorLibrary.DataModel;

namespace SystemMonitorLibrary.Interfaces
{
    public interface ISystemMonitor
    {
        ICPUUsageMonitor CpuUsageMonitor { get;}
        IDiskUsageMonitor DiskUsageMonitor { get;}
        IRAMUsageMonitor RAMUsageMonitor { get;}
        async Task<MonitorMetrics> GetMetricsAsync()
        {
            decimal cpuUsagePercentage = CpuUsageMonitor.GetCPUUsagePercentage();
            (_, _, _, decimal ramUsage) = RAMUsageMonitor.GetRAMUsage();
            (_, _, _, decimal diskUsage) = DiskUsageMonitor.GetDiskUsage();

            MonitorMetrics monitorMetrics = new MonitorMetrics
            {
                CpuPercent = cpuUsagePercentage,
                RamUsagePercent = ramUsage * 100,
                DiskUsagePercent = diskUsage * 100
            };
            return monitorMetrics;
        }

    }
}
