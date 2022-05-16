namespace Adfenix.MetricsScraper.Services
{
    internal interface IServiceDiscovery
    {
        Task<List<string>> GetServiceEndpoints(string service);
    }
}
