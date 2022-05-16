using System.Text.Json.Serialization;

namespace Adfenix.MetricsScraper.Models.Zendesk
{
    public class CountResponse
    {
        [JsonPropertyName("count")]
        public string Count { get; set; }
    }
}
