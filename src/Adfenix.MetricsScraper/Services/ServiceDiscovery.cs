
using ConfigurationManager = Adfenix.MetricsScraper.Configuration.ConfigurationManager;
using Microsoft.Extensions.Configuration;

namespace Adfenix.MetricsScraper.Services
{
    internal class ServiceDiscovery : IServiceDiscovery
    {
        private readonly Dictionary<string, List<string>> _endpoints = new();
        private const string EndpointsConfigSection = "Endpoints:Multiple";
        public ServiceDiscovery()
        {
            _endpoints = ConfigurationManager.Configuration
                .GetSection(EndpointsConfigSection)
                .Get<Dictionary<string, List<string>>>();
        }
        public Task<List<string>> GetServiceEndpoints(string service)
        {
            return Task.FromResult(_endpoints[service]);
        }
    }
}
