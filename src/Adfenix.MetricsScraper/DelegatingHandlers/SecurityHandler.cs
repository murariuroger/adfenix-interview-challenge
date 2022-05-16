using Adfenix.MetricsScraper.Security;

namespace Adfenix.MetricsScraper.DelegatingHandlers
{
    public class SecurityHandler : DelegatingHandler
    {
        private readonly IAuthProvider _authProvider;

        public SecurityHandler(IAuthProvider authProvider)
        {
            _authProvider = authProvider ?? throw new ArgumentNullException(nameof(authProvider));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _authProvider.GetAuthToken();
            request.Headers.Add("Authorization", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
