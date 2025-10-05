using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SystemMonitorLibrary.DataModel;
using SystemMonitorLibrary.Interfaces;

namespace SystemMonitorLibrary.PlugIns
{
    public class HttpPosterPlugin : IMonitorPlugin, IDisposable
    {
        public string PlugInName => "HttpPoster";

        private readonly HttpClient _client;
        private readonly string _endpoint;

        public HttpPosterPlugin(string endpoint)
        {
            _endpoint = endpoint;
            _client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
        }

        public async Task OnMetricsAsync(MonitorMetrics metrics)
        {
            if (string.IsNullOrWhiteSpace(_endpoint)) return;

            var payload = new
            {
                cpu = metrics.CpuPercent,
                ram_used = metrics.RamUsagePercent,
                disk_used = metrics.DiskUsagePercent,
                timestamp = metrics.TimestampUTC
            };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var res = await _client.PostAsync(_endpoint, content);
            }
            catch
            {
               
            }
        }

        public void Dispose() => _client?.Dispose();
    }

}
