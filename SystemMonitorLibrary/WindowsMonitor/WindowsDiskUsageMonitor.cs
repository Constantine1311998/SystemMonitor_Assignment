using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using SystemMonitorLibrary.Extensions;
using SystemMonitorLibrary.Interfaces;

namespace SystemMonitorLibrary.WindowsMonitor
{
    [SupportedOSPlatform("windows")]
    public class WindowsDiskUsageMonitor : IDiskUsageMonitor
    {
        [SupportedOSPlatform("windows")]
        public (decimal total, decimal free, decimal used, decimal usage) GetDiskUsage()
        {
            decimal totalDriveSpace = 0;
            decimal freeDriveSpace = 0;
            decimal usedDriveSpace = 0;
            decimal usageDriveSpace = 0;
            foreach (var drive in DriveInfo.GetDrives())
            {

                if (drive.IsReady)
                {
                    totalDriveSpace += drive.TotalSize; // will get in bytes
                    freeDriveSpace += drive.TotalFreeSpace;
                }
            }
            usedDriveSpace = totalDriveSpace - freeDriveSpace;
            usageDriveSpace = usedDriveSpace / totalDriveSpace;

            return (totalDriveSpace.ConvertToGB(Enums.DataConversionUnit.Byte).RoundToSettingsPrecision(),
                    freeDriveSpace.ConvertToGB(Enums.DataConversionUnit.Byte).RoundToSettingsPrecision(),
                usedDriveSpace.ConvertToGB(Enums.DataConversionUnit.Byte).RoundToSettingsPrecision(),
                usageDriveSpace.RoundToSettingsPrecision());
        }
    }
}
