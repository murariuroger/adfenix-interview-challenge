using Adfenix.ZendeskMock.Extensions;

namespace Adfenix.ZendeskMock.Middlewares
{
    internal class BadSecurityMiddleware
    {
        private readonly RequestDelegate _next;
        private const string _authorizationHeaderName = "Authorization";
        private const string _authorizationExpectedHeader = "Basic token";

        public BadSecurityMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(!context.HeaderEquals(_authorizationHeaderName, _authorizationExpectedHeader))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }
            
            await _next(context);
        }
    }
}
