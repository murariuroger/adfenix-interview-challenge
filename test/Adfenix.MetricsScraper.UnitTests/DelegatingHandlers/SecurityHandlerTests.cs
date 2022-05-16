using Adfenix.MetricsScraper.DelegatingHandlers;
using Adfenix.MetricsScraper.Security;
using Moq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Adfenix.MetricsScraper.UnitTests.DelegatingHandlers
{
    public class SecurityHandlerTests
    {
        private readonly SecurityHandler _sut;
        private const string AuthTokenValue = nameof(AuthTokenValue);

        public SecurityHandlerTests()
        {
            var mockAuthProvider = new Mock<IAuthProvider>();
            mockAuthProvider.Setup(_ => _.GetAuthToken())
                .ReturnsAsync(AuthTokenValue);

            _sut = new(mockAuthProvider.Object)
            {
                InnerHandler = new DummyHandler()
            };
        }

        [Fact]
        public async Task Should_Setup_Authorization_Header_With_Token_From_AuthProvider()
        {
            // Arrange
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://test.com/");
            var invoker = new HttpMessageInvoker(_sut);

            // Act
            var result = await invoker.SendAsync(httpRequestMessage, new CancellationToken());

            // Assert
            Assert.Equal(AuthTokenValue, httpRequestMessage.Headers.GetValues("Authorization").First());
        }

        private class DummyHandler : DelegatingHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.OK), cancellationToken);
            }
        }

    }
}
