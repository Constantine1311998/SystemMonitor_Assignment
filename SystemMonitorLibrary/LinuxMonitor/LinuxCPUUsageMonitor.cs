using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMonitorLibrary.Interfaces;

namespace SystemMonitorLibrary.LinuxMonitor
{
    public class LinuxCPUUsageMonitor : ICPUUsageMonitor
    {
        public LinuxCPUUsageMonitor(int scanInterval)
        {
            this.ScanInterval = scanInterval;
        }
        public int ScanInterval { get; set; }
        public decimal GetCPUUsagePercentage()
        {
            var preUsage = ReadCpuStats();
            Thread.Sleep(ScanInterval);
            var postUsage = ReadCpuStats();
            return CalculateCpuUsage(preUsage, postUsage);
        }


        private (long idle, long total) ReadCpuStats()
        {
            string[] parts = File.ReadAllLines("/proc/stat")[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            long user = long.Parse(parts[1]);
            long nice = long.Parse(parts[2]);
            long system = long.Parse(parts[3]);
            long idle = long.Parse(parts[4]);
            long iowait = long.Parse(parts[5]);
            long irq = long.Parse(parts[6]);
            long softirq = long.Parse(parts[7]);

            long total = user + nice + system + idle + iowait + irq + softirq;
            return (idle + iowait, total);
        }

        private decimal CalculateCpuUsage((long idle, long total) start, (long idle, long total) end)
        {
            long idleDiff = end.idle - start.idle;
            long totalDiff = end.total - start.total;
            return (decimal)(100.0 * (1.0 - idleDiff / totalDiff));
        }
    }
}
