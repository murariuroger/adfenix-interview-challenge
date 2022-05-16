using Adfenix.MetricsScraper.Configuration;
using Adfenix.MetricsScraper.Extensions;
using Adfenix.MetricsScraper.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.ConfigureLogging()
    .AddServices()
    .AddHttpClients(ConfigurationManager.Configuration);

var sp = services.BuildServiceProvider();
var scraper = sp.GetRequiredService<IScraper>();

await scraper.Scrape();