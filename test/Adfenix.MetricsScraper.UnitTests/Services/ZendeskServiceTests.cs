using Adfenix.MetricsScraper.Models.Zendesk;
using Adfenix.MetricsScraper.Services;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Adfenix.MetricsScraper.UnitTests.Services
{
    public class ZendeskServiceTests
    {
        [Fact]
        public async Task Should_Return_Counter()
        {
            // Arrange
            var series = new CountResponse
            {
                Count = "10"
            };
            var contentBody = new StringContent(JsonSerializer.Serialize(series));

            var httpClientFactory = HttpClientFactoryHelper.ForgeHttpClientFactory(HttpStatusCode.OK, contentBody);
            var sut = new ZendeskService(httpClientFactory);

            // Act 
            var res = await sut.GetCountAsync();

            // Assert
            Assert.NotNull(res);
            Assert.Equal(series.Count, res.Count);
        }

        [Fact]
        public async Task Should_Throw_ForGetCountAsync_If_StatusCode_Not_200()
        {
            // Arrange
            var httpClientFactory = HttpClientFactoryHelper.ForgeHttpClientFactory(HttpStatusCode.Forbidden, new StringContent("test"));
            var sut = new ZendeskService(httpClientFactory);

            // Act & Assert
            await Assert.ThrowsAnyAsync<HttpRequestException>(async () => await sut.GetCountAsync());
        }
    }
}
