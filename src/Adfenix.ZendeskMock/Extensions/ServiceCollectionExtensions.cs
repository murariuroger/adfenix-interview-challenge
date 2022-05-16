using Adfenix.ZendeskMock.Services;

namespace Adfenix.ZendeskMock.Extensions
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
