namespace Adfenix.MetricsScraper.Security
{
    internal class AuthProvider : IAuthProvider
    {
        public Task<string> GetAuthToken() => Task.FromResult("Basic token");
    }
}
