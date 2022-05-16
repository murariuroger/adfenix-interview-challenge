using Adfenix.MetricsScraper.Models.Visualiser;
using Adfenix.MetricsScraper.Models.Visualiser.Responses;
using Adfenix.MetricsScraper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Adfenix.MetricsScraper.UnitTests.Services
{
    public  class VisualiserServiceUnitTests
    {
        [Fact]
        public async Task Should_Return_Series()
        {
            // Arrange
            var series = new SeriesResponse
            {
                Series = new List<Series>
                {
                    new Series() 
                    { 
                        Metric = "Metrics.UnitTest", 
                        Type = "type", 
                        Points = new List<List<ulong>>() 
                        { 
                            new List<ulong> { 987654321, 2 }
                        }
                    }
                }
            };
            var contentBody = new StringContent(JsonSerializer.Serialize(series));

            var httpClientFactory = HttpClientFactoryHelper.ForgeHttpClientFactory(HttpStatusCode.OK, contentBody);
            var sut = new VisualiserService(httpClientFactory);

            // Act 
            var res = await sut.GetSeries();

            // Assert
            Assert.NotNull(res);
            Assert.Equal(series.Series.First().Metric, res.Series.First().Metric);
            Assert.Equal(series.Series.First().Type, res.Series.First().Type);
            Assert.Equal(series.Series.First().Points.Count, res.Series.First().Points.Count);
        }

        [Fact]
        public async Task Should_Throw_ForGet_If_StatusCode_Not_200()
        {
            // Arrange
            var httpClientFactory = HttpClientFactoryHelper.ForgeHttpClientFactory(HttpStatusCode.Forbidden, new StringContent("test"));
            var sut = new VisualiserService(httpClientFactory);

            // Act & Assert
            await Assert.ThrowsAnyAsync<HttpRequestException>(async () => await sut.GetSeries());
        }

        [Fact]
        public async Task Should_Throw_ForAdd_If_StatusCode_Not_200()
        {
            // Arrange
            var httpClientFactory = HttpClientFactoryHelper.ForgeHttpClientFactory(HttpStatusCode.Forbidden, new StringContent("test"));
            var sut = new VisualiserService(httpClientFactory);

            // Act & Assert
            await Assert.ThrowsAnyAsync<HttpRequestException>(async () => await sut.AddSeries(new() { }));
        }

    }
}
