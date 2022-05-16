using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Adfenix.MetricsScraper.UnitTests
{
    internal static class HttpClientFactoryHelper
    {
        public static IHttpClientFactory ForgeHttpClientFactory(HttpStatusCode statusCode, HttpContent httpContent)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = httpContent
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://example.com");

            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(client);

            return mockFactory.Object;
        }
    }
}
