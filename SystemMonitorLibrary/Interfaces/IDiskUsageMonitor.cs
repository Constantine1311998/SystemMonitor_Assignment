namespace SystemMonitorLibrary.Interfaces
{
    public interface IDiskUsageMonitor
    {
        public (decimal total, decimal free, decimal used, decimal usage) GetDiskUsage();
    }
}
