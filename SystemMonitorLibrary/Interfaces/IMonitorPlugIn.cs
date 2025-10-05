using SystemMonitorLibrary.DataModel;

namespace SystemMonitorLibrary.Interfaces
{
    public interface IMonitorPlugin
    {
        string PlugInName { get; }
        Task OnMetricsAsync(MonitorMetrics metrics);
    }
}
