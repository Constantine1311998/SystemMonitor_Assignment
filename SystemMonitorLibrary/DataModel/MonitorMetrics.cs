namespace SystemMonitorLibrary.DataModel
{
    public class MonitorMetrics
    {
        public decimal CpuPercent { get; set; }
        public decimal RamUsagePercent { get; set; }
        public decimal DiskUsagePercent { get; set; }
        public DateTime TimestampUTC { get; set; } = DateTime.UtcNow;
    }
}
