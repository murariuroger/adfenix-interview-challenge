using Adfenix.CampaignServersMock.Services;

namespace Adfenix.CampaignServersMock.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ICounterService, CounterService>();

            return services;
        }
    }
}
