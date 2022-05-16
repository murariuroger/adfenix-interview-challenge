using System.Text.Json.Serialization;

namespace Adfenix.MetricsScraper.Models.Visualiser.Responses
{
    public class SeriesResponse
    {
        [JsonPropertyName("series")]
        public IEnumerable<Series> Series { get; set; }
    }
}
