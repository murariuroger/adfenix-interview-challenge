using Adfenix.Visualiser.Models;
using System.Text.Json.Serialization;

namespace Adfenix.VisualiserMock.Models.Requests
{
    public class AddSeriesRequest
    {
        [JsonPropertyName("series")]
        public List<Series> Series { get; set; }
    }
}
