using System.Text.Json.Serialization;

namespace Adfenix.ZendeskMock.Models
{
    public class CountResponse
    {
        [JsonPropertyName("count")]
        public string Count { get; set; }
    }
}
