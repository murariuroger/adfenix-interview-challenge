using Adfenix.MetricsScraper.Models.Visualiser.Requests;
using Adfenix.MetricsScraper.Models.Zendesk;
using Adfenix.MetricsScraper.Services;
using Moq;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Adfenix.MetricsScraper.UnitTests.Services
{
    public class ScraperTests
    {
        private readonly Mock<IServiceDiscovery> _serviceDiscoveryMock;
        private readonly Mock<IVisualiserService> _visualiserMockService;
        private readonly Mock<IZendeskService> _zendeskMockService;
        private readonly Mock<ICampaignService> _campaignMockService;
        private readonly Mock<ILogger> _loggerMock;
        private readonly Scraper _sut;

        public ScraperTests()
        {
            _serviceDiscoveryMock = new Mock<IServiceDiscovery>();
            _serviceDiscoveryMock
                .Setup(_ => _.GetServiceEndpoints(It.IsAny<string>()))
                .ReturnsAsync(new List<string>()
                {
                    "http://0.example.com",
                    "http://1.example.com",
                    "http://2.example.com"
                });

            _visualiserMockService = new Mock<IVisualiserService>();
            _zendeskMockService = new Mock<IZendeskService>();
            _zendeskMockService
                .Setup(_ => _.GetCountAsync())
                .ReturnsAsync(new CountResponse { Count = "2" });

            _campaignMockService = new Mock<ICampaignService>();
            _campaignMockService
                .Setup(_ => _.GetCampaignCountAsync(It.IsAny<string>()))
                .ReturnsAsync("10");

            _loggerMock = new Mock<ILogger>();
            _loggerMock
                .Setup(_ => _.ForContext<Scraper>())
                .Returns(_loggerMock.Object);

            _sut = new Scraper(_serviceDiscoveryMock.Object, _visualiserMockService.Object, _zendeskMockService.Object,
                _campaignMockService.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Should_Fetch_Data_From_All_Campaign_Servers_And_Send_It_To_Visualiser()
        {
            // Act 
            await _sut.Scrape();

            // Assert
            _campaignMockService.Verify(_ => _.GetCampaignCountAsync(It.IsAny<string>()), Times.Exactly(3));
            _visualiserMockService.Verify(_ => _.AddSeries(It.Is<AddSeriesRequest>(s => s.Series[0].Points[0][1] == 10)), Times.Exactly(3));
            _visualiserMockService.Verify(_ => _.AddSeries(It.IsAny<AddSeriesRequest>()), Times.Exactly(4));
        }

        [Fact]
        public async Task Should_Fetch_Data_From_Zendesk_And_Send_It_To_Visualiser()
        {
            // Act 
            await _sut.Scrape();

            // Assert
            _zendeskMockService.Verify(_ => _.GetCountAsync(), Times.Once);
            _visualiserMockService.Verify(_ => _.AddSeries(It.Is<AddSeriesRequest>(s => s.Series[0].Points[0][1] == 2)), Times.Once);
            _visualiserMockService.Verify(_ => _.AddSeries(It.IsAny<AddSeriesRequest>()), Times.Exactly(4));
        }

        [Fact]
        public async Task Should_Log_Campaign_Queue_Size()
        {
            // Act 
            await _sut.Scrape();

            // Assert
            _loggerMock.Verify(_ => _.Information(It.Is<string>(_ => _.Contains("Campaign Queue Size")), It.IsAny<int>(), It.IsAny<string>()), Times.Exactly(3));
        }

        [Fact]
        public async Task Should_Log_Zendesk_Ticket_Count()
        {
            // Act 
            await _sut.Scrape();

            // Assert
            _loggerMock.Verify(_ => _.Information(It.Is<string>(_ => _.Contains("Zendesk Engineering Ticket count")), It.IsAny<string>()), Times.Once);
        }
    }
}
