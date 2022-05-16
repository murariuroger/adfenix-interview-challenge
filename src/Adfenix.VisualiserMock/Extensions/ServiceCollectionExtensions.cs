using Adfenix.Visualiser.Services;

namespace Adfenix.Visualiser.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ISeriesService, SeriesService>();

            return services;
        }
    }
}
