using System.Runtime.InteropServices;
using SystemMonitorLibrary.Interfaces;
using SystemMonitorLibrary.WindowsMonitor;
using SystemMonitorLibrary.LinuxMonitor;
using SystemMonitorLibrary.Config;

namespace SystemMonitorLibrary.SystemMonitorFactory
{
    public class SystemMonitorFactory
    {
        private SystemMonitorFactory() { }
        private static readonly Lazy<SystemMonitorFactory> _lazyInstance = new Lazy<SystemMonitorFactory>(() => new SystemMonitorFactory());
        public static SystemMonitorFactory Instance { get => _lazyInstance.Value; }


        public ISystemMonitor CreateSystemMonitor(OSPlatform platform)
        {
            ISystemMonitor systemMonitor = null;
            if (platform == OSPlatform.Windows)
            {
                systemMonitor = new SystemMonitorWindows(new WindowsCPUUsageMonitor(SettingsManager.Settings.ScanInterval), new WindowsDiskUsageMonitor(), new WindowsRAMUsageMonitor());
            }
            else if (platform == OSPlatform.Linux)
            {
                systemMonitor = new SystemMonitorLinux(new LinuxCPUUsageMonitor(SettingsManager.Settings.ScanInterval), new LinuxDiskUsageMonitor(), new LinuxRAMUsageMonitor() );
            }
            else
            {
                throw new ArgumentException("OS Not supported or Recognized");
            }
            return systemMonitor;
        }

    }
}
