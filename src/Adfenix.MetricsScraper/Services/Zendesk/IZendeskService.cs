using Adfenix.MetricsScraper.Models.Zendesk;

namespace Adfenix.MetricsScraper.Services
{
    internal interface IZendeskService
    {
        Task<CountResponse> GetCountAsync();
    }
}
