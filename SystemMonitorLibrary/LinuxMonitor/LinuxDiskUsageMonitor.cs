using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemMonitorLibrary.Extensions;
using SystemMonitorLibrary.Interfaces;

namespace SystemMonitorLibrary.LinuxMonitor
{
    public class LinuxDiskUsageMonitor : IDiskUsageMonitor
    {
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
