using Adfenix.MetricsScraper.Common;
using Adfenix.MetricsScraper.Models.Visualiser.Requests;
using Adfenix.MetricsScraper.Models.Visualiser.Responses;
using System.Text;
using System.Text.Json;

namespace Adfenix.MetricsScraper.Services
{
    internal class VisualiserService : IVisualiserService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "randomstring";
        public VisualiserService(IHttpClientFactory httpClientFactory)
        {
            _ = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClient = httpClientFactory.CreateClient(AppConstants.VisualizerHttpClient);
        }

        public async Task<SeriesResponse> GetSeries()
        {
            using var httpResponse = await _httpClient.GetAsync("");
            httpResponse.EnsureSuccessStatusCode();
            var responseString = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<SeriesResponse>(responseString);
        }

        public async Task AddSeries(AddSeriesRequest request)
        {
            var serializedJson = JsonSerializer.Serialize(request);
            using var httpResponse = await _httpClient.PostAsync($"?api_key={ApiKey}", new StringContent(serializedJson, Encoding.UTF8, "application/json"));
            httpResponse.EnsureSuccessStatusCode();
        }
    }
}
