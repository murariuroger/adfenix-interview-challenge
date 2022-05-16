using Adfenix.MetricsScraper.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Adfenix.MetricsScraper.UnitTests.Services
{
    public class CampaignServiceTests
    {
        [Fact]
        public async Task CampaignService_Should_Returned_Failed_If_Response_Cannot_Be_Parsed()
        {
            // Arrange
            var httpClientFactory = HttpClientFactoryHelper.ForgeHttpClientFactory(HttpStatusCode.Forbidden, new StringContent("nocount: 2"));
            var sut = new CampaignService(httpClientFactory);

            // Act
            var res = await sut.GetCampaignCountAsync("http://example.com");

            // Assert
            Assert.NotNull(res);
            Assert.Equal("failed", res);
        }

        [Fact]
        public async Task CampaignService_Should_Returned_Failed_If_StatusCode_Not_200()
        {
            // Arrange
            var httpClientFactory = HttpClientFactoryHelper.ForgeHttpClientFactory(HttpStatusCode.Forbidden, new StringContent("new count: 2"));
            var sut = new CampaignService(httpClientFactory);

            // Act
            var res = await sut.GetCampaignCountAsync("http://example.com");

            // Assert
            Assert.NotNull(res);
            Assert.Equal("failed", res);
        }

        public static IEnumerable<object[]> Contents => new List<object[]>
        {
            new object[] { "new count: 2",  "2"},
            new object[] { "new count: 20", "20"},
            new object[] { "new count: 100", "100"},
            new object[] { "new count: 1000", "1000"},
            new object[] { "new count: 99999", "99999"},
            new object[] { "new count: 9999999", "9999999" }
        };

        [Theory]
        [MemberData(nameof(Contents))]
        public async Task CampaignService_Should_Be_Able_To_Parse_Counter(string content, string output) 
        {
            // Arrange
            var httpClientFactory = HttpClientFactoryHelper.ForgeHttpClientFactory(HttpStatusCode.OK, new StringContent(content));
            var sut = new CampaignService(httpClientFactory);

            // Act 
            var res = await sut.GetCampaignCountAsync("http://example.com");

            // Assert
            Assert.NotNull(res);
            Assert.Equal(output, res);
        }
    }
}
