using Adfenix.MetricsScraper.Common;
using Adfenix.MetricsScraper.DelegatingHandlers;
using Adfenix.MetricsScraper.Security;
using Adfenix.MetricsScraper.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using System.Net.Http.Headers;

namespace Adfenix.MetricsScraper.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureLogging(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ILogger>(_ => Log.Logger);
            services.AddSingleton<IServiceDiscovery, ServiceDiscovery>();
            services.AddSingleton<IZendeskService, ZendeskService>();
            services.AddSingleton<ICampaignService, CampaignService>();
            services.AddSingleton<IVisualiserService, VisualiserService>();
            services.AddSingleton<IAuthProvider, AuthProvider>();
            services.AddSingleton<IScraper, Scraper>();
            services.AddTransient<SecurityHandler>();

            return services;
        }

        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var retryPolicy = HttpPolicyExtensions
                      .HandleTransientHttpError()
                      .RetryAsync(3);

            services.AddHttpClient(AppConstants.ZendeskHttpClient, client =>
            {
                var baseAddress = configuration.GetValue<string>("Endpoints:Single:ZendeskMock");
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddHttpMessageHandler<SecurityHandler>()
            .AddPolicyHandler(retryPolicy);

            services.AddHttpClient(AppConstants.VisualizerHttpClient, client =>
            {
                var baseAddress = configuration.GetValue<string>("Endpoints:Single:VisualiserMock");
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddPolicyHandler(retryPolicy);

            services.AddHttpClient(AppConstants.CampaignServerHttpClient, client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            })
            .AddPolicyHandler(retryPolicy);

            return services;
        }
    }
}
