namespace SystemMonitorLibrary.Interfaces
{
    public interface ICPUUsageMonitor
    {
        int ScanInterval { get; set; }
        decimal GetCPUUsagePercentage();
    }
}
