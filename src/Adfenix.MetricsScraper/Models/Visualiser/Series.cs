using System.Text.Json.Serialization;

namespace Adfenix.MetricsScraper.Models.Visualiser
{
    public class Series
    {
        [JsonPropertyName("metric")]
        public string Metric { get; set; }

        [JsonPropertyName("points")]
        public List<List<ulong>> Points { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
