using Adfenix.Visualiser.Models;
using System.Text.Json.Serialization;

namespace Adfenix.VisualiserMock.Models.Responses
{
    public class SeriesResponse
    {
        [JsonPropertyName("series")]
        public IEnumerable<Series> Series { get; set; }
    }
}
