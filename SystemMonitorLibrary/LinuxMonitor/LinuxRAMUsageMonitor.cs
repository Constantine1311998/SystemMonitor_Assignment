using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMonitorLibrary.Extensions;
using SystemMonitorLibrary.Interfaces;

namespace SystemMonitorLibrary.LinuxMonitor
{
    public class LinuxRAMUsageMonitor : IRAMUsageMonitor
    {

        public (decimal totalRam, decimal freeRAM, decimal usedRAM, decimal ramUsage) GetRAMUsage()
        {
            var memInfo = new Dictionary<string, long>();

            foreach (var line in File.ReadLines("/proc/meminfo"))
            {
                var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2) continue;

                string key = parts[0].Trim();
                string valuePart = parts[1].Trim().Split(' ')[0];
                if (long.TryParse(valuePart, out long valueKb))
                {
                    memInfo[key] = valueKb; // value in KB
                }
            }
            decimal totalRam = (decimal)memInfo.GetValueOrDefault("MemTotal", 0);
            decimal freeRam = (decimal)memInfo.GetValueOrDefault("MemFree", 0);
            decimal usedRam = totalRam - freeRam;
            decimal usageRam = usedRam / totalRam;
            return(totalRam.ConvertToGB(Enums.DataConversionUnit.KiloByte).RoundToSettingsPrecision(), 
                freeRam.ConvertToGB(Enums.DataConversionUnit.KiloByte).RoundToSettingsPrecision(), 
                usedRam.ConvertToGB(Enums.DataConversionUnit.KiloByte).RoundToSettingsPrecision(),
                usageRam.RoundToSettingsPrecision());
        }
    }
}
