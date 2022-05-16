namespace Adfenix.MetricsScraper.Security
{
    public interface IAuthProvider
    {
        Task<string> GetAuthToken();
    }
}
