using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMonitorLibrary.DataModel;
using SystemMonitorLibrary.Interfaces;
using SystemMonitorLibrary.LinuxMonitor;

namespace SystemMonitorLibrary.SystemMonitorFactory
{
    public class SystemMonitorLinux : ISystemMonitor
    {
        public ICPUUsageMonitor CpuUsageMonitor { get; set; }

        public IDiskUsageMonitor DiskUsageMonitor { get; set; }

        public IRAMUsageMonitor RAMUsageMonitor { get; set; }

        public SystemMonitorLinux(LinuxCPUUsageMonitor cpuUsageMonitor, LinuxDiskUsageMonitor diskUsageMonitor, LinuxRAMUsageMonitor ramUsageMonitor)
        {
            this.CpuUsageMonitor = cpuUsageMonitor;
            this.RAMUsageMonitor = ramUsageMonitor;
            this.DiskUsageMonitor = diskUsageMonitor;
        }
    }
}
