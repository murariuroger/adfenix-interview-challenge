namespace Adfenix.ZendeskMock.Extensions
{
    internal static class HttpContextExtensions
    {
        public static bool HeaderEquals(this HttpContext context, string headerName, string expected)
        {
            return context.Request.Headers.Keys.Any(k => k == headerName) && context.Request.Headers[headerName] == expected;
        }
    }
}
