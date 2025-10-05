using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMonitorLibrary.DataModel;
using SystemMonitorLibrary.Interfaces;
using SystemMonitorLibrary.WindowsMonitor;

namespace SystemMonitorLibrary.SystemMonitorFactory
{
    internal class SystemMonitorWindows : ISystemMonitor
    {
        public ICPUUsageMonitor CpuUsageMonitor { get;set; }

        public IDiskUsageMonitor DiskUsageMonitor { get; set; }

        public IRAMUsageMonitor RAMUsageMonitor { get; set; }

        public SystemMonitorWindows(WindowsCPUUsageMonitor cpuUsageMonitor, WindowsDiskUsageMonitor diskUsageMonitor, WindowsRAMUsageMonitor rAMUsageMonitor)
        {
            this.CpuUsageMonitor = cpuUsageMonitor;
            this.RAMUsageMonitor = rAMUsageMonitor;
            this.DiskUsageMonitor = diskUsageMonitor;
        }
    }
}
