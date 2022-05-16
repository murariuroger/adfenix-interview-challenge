using Adfenix.MetricsScraper.Models.Visualiser;
using Adfenix.MetricsScraper.Models.Visualiser.Requests;
using Serilog;

namespace Adfenix.MetricsScraper.Services
{
    internal class Scraper : IScraper
    {
        private readonly IServiceDiscovery _serviceDiscovery;
        private readonly IVisualiserService _visualiserService;
        private readonly IZendeskService _zendeskService;
        private readonly ICampaignService _campaignService;
        private readonly ILogger _logger;

        public Scraper(IServiceDiscovery serviceDiscovery, IVisualiserService visualiserService, IZendeskService zendeskService,
            ICampaignService campaignService, ILogger logger)
        {
            _serviceDiscovery = serviceDiscovery ?? throw new ArgumentNullException(nameof(serviceDiscovery));
            _visualiserService = visualiserService ?? throw new ArgumentNullException(nameof(visualiserService));
            _zendeskService = zendeskService ?? throw new ArgumentNullException(nameof(zendeskService));
            _campaignService = campaignService ?? throw new ArgumentNullException(nameof(campaignService));
            _logger = logger?.ForContext<Scraper>() ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Scrape()
        {
            var campaignServers = await _serviceDiscovery.GetServiceEndpoints("CampaignServersMock");
            ulong unixTimestamp = Convert.ToUInt64(DateTimeOffset.Now.ToUnixTimeSeconds());

            for (int i = 0; i < campaignServers.Count; i++)
            {
                var campaignCount = await _campaignService.GetCampaignCountAsync(campaignServers[i]);

                var campaignSeriesAddReq = GetAddSeriesRequest($"Campaign.{i + 1}", unixTimestamp, campaignCount);
                await _visualiserService.AddSeries(campaignSeriesAddReq);

                _logger.Information("Server: {serverIndex}   Campaign Queue Size: {campaignCount}", i + 1, campaignCount);
            }

            var zendeskCount = await _zendeskService.GetCountAsync();
            var zendeskSeriesAddRequest = GetAddSeriesRequest("Zendesk.Metric", unixTimestamp, zendeskCount.Count);
            await _visualiserService.AddSeries(zendeskSeriesAddRequest);

            _logger.Information("Zendesk Engineering Ticket count: {zendeskCount}", zendeskCount.Count);
        }

        private AddSeriesRequest GetAddSeriesRequest(string metric, ulong unixTime, string count) => new AddSeriesRequest
        {
            Series = new()
            {
                new Series
                {
                    Metric = metric,
                    Points = new List<List<ulong>>
                    {
                        new() { unixTime, Convert.ToUInt64(count) }
                    },
                    Type = "count"
                }
            }
        };
    }
}
