using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SystemMonitorLibrary.Interfaces
{
    public interface IRAMUsageMonitor
    {
        (decimal totalRam, decimal freeRAM, decimal usedRAM, decimal ramUsage) GetRAMUsage();
    }
    
}
