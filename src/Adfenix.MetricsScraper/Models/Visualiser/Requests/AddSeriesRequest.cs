using System.Text.Json.Serialization;

namespace Adfenix.MetricsScraper.Models.Visualiser.Requests
{
    public class AddSeriesRequest
    {
        [JsonPropertyName("series")]
        public List<Series> Series { get; set; }
    }
}
