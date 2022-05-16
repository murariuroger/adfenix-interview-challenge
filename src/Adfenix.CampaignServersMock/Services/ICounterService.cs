namespace Adfenix.CampaignServersMock.Services
{
    public interface ICounterService
    {
        Task<long> GetCounterValueAsync(string server);
    }
}
