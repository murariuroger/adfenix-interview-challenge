namespace Adfenix.MetricsScraper.Services
{
    internal interface ICampaignService
    {
        Task<string> GetCampaignCountAsync(string serverEndpoint);
    }
}
