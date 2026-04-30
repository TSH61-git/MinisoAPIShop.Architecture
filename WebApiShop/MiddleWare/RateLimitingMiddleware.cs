using System.Collections.Concurrent;
using System.Net;

namespace WebApiShop.MiddleWare
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, (DateTime WindowStart, int RequestCount)> _cache = new();

        // הגדרות המכסה
        private const int MaxRequests = 10; // מספר בקשות מקסימלי
        private static readonly TimeSpan WindowSize = TimeSpan.FromMinutes(1); // גודל החלון

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var now = DateTime.UtcNow;

            var clientData = _cache.GetOrAdd(clientIp, (now, 0));

            // אם עבר זמן החלון, אפס את המונה
            if (now - clientData.WindowStart > WindowSize)
            {
                clientData = (now, 1);
            }
            else
            {
                clientData.RequestCount++;
            }

            _cache[clientIp] = clientData;

            if (clientData.RequestCount > MaxRequests)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }

            await _next(context);
        }
    }
}