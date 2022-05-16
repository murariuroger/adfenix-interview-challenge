using Microsoft.Extensions.Configuration;

namespace Adfenix.MetricsScraper.Configuration
{
    internal static class ConfigurationManager
    {
        public static IConfiguration Configuration { get; }
        static ConfigurationManager()
        {
            Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();
        }
    }
}
