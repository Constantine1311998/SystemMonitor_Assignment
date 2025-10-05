using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using SystemMonitorLibrary.Interfaces;

namespace SystemMonitorLibrary.WindowsMonitor
{
    [SupportedOSPlatform("windows")] // Mark the class as Windows-only
    public class WindowsCPUUsageMonitor : ICPUUsageMonitor
    {
        public WindowsCPUUsageMonitor(int scanInterval)
        {
            this.ScanInterval = scanInterval;
        }
        public int ScanInterval { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scanInterval">scan interval in ms</param>
        /// <returns></returns>
        [SupportedOSPlatform("windows")] // Mark the method as Windows-only
        public decimal GetCPUUsagePercentage()
        {
            using (PerformanceCounter pc = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                pc.NextValue();
                Thread.Sleep(ScanInterval);
                return (decimal)pc.NextValue();
            }
        }
    }
}
