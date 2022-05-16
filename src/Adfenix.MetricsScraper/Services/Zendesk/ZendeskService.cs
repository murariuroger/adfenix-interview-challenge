using Adfenix.MetricsScraper.Common;
using Adfenix.MetricsScraper.Models.Zendesk;
using System.Text.Json;

namespace Adfenix.MetricsScraper.Services
{
    internal class ZendeskService : IZendeskService
    {
        private readonly HttpClient _httpClient;
        public ZendeskService(IHttpClientFactory httpClientFactory)
        {
            _ = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClient = httpClientFactory.CreateClient(AppConstants.ZendeskHttpClient);
        }

        public async Task<CountResponse> GetCountAsync()
        {
            using var httpResponse = await _httpClient.GetAsync("");
            httpResponse.EnsureSuccessStatusCode();
            var responseString = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CountResponse>(responseString);
        }
    }
}
