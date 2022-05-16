using Adfenix.MetricsScraper.Common;
using System.Text.RegularExpressions;

namespace Adfenix.MetricsScraper.Services
{
    internal class CampaignService : ICampaignService
    {
        private readonly HttpClient _httpClient;
        public CampaignService(IHttpClientFactory httpClientFactory)
        {
            _ = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClient = httpClientFactory.CreateClient(AppConstants.CampaignServerHttpClient);
        }

        public async Task<string> GetCampaignCountAsync(string serverEndpoint)
        {
            string responseString;
            try
            {
                using var httpResponse = await _httpClient.GetAsync(serverEndpoint);
                httpResponse.EnsureSuccessStatusCode();
                responseString = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return "failed";
            }

            return GetCount(responseString);
        }

        private string GetCount(string requestBody)
        {
            const string newCount = "new count: (.*)";
            var match = new Regex(newCount, RegexOptions.IgnoreCase).Match(requestBody);
            if (!match.Success)
                return "failed";

            return match.Groups[1].Value;
        }

    }
}
