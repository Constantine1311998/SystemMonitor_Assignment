using Microsoft.TeamFoundation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMonitorLibrary.Interfaces;
using System.Management;
using System.Runtime.Versioning;
using SystemMonitorLibrary.Extensions;


namespace SystemMonitorLibrary.WindowsMonitor
{
    [SupportedOSPlatform("windows")]
    public class WindowsRAMUsageMonitor : IRAMUsageMonitor
    {
        [SupportedOSPlatform("windows")]
        public (decimal totalRam, decimal freeRAM, decimal usedRAM, decimal ramUsage) GetRAMUsage()
        {
            var wql = new ObjectQuery("SELECT TotalVisibleMemorySize,FreePhysicalMemory FROM Win32_OperatingSystem");
            var searcher = new ManagementObjectSearcher(wql);

            foreach (ManagementObject result in searcher.Get())
            {
               
                decimal totalRam = Convert.ToDecimal(result["TotalVisibleMemorySize"]);
                decimal freeRam = Convert.ToDecimal(result["FreePhysicalMemory"]);
                decimal usedRam = totalRam - freeRam;
                decimal ramUsage = (decimal)usedRam / (decimal)totalRam;
                return (totalRam.ConvertToGB(Enums.DataConversionUnit.KiloByte).RoundToSettingsPrecision()
                    , freeRam.ConvertToGB(Enums.DataConversionUnit.KiloByte).RoundToSettingsPrecision(), 
                    usedRam.ConvertToGB(Enums.DataConversionUnit.KiloByte).RoundToSettingsPrecision(), 
                    ramUsage.RoundToSettingsPrecision());
            }
            // Consider throwing or returning a default value if no result is found
            throw new InvalidOperationException("Unable to retrieve RAM usage information.");
        }
    }
}
