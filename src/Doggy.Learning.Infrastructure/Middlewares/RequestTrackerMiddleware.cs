using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Doggy.Learning.Infrastructure.Middlewares
{
    public class RequestTrackerMiddleware : MiddlewareBase
    {
        private readonly ILogger<RequestTrackerMiddleware> _logger;

        public RequestTrackerMiddleware(ILogger<RequestTrackerMiddleware> logger, RequestDelegate next)
            : base(next)
        {
            _logger = logger;
        }

        protected override async Task HandleRequest(HttpContext context)
        {
            var body = await ReadRequestBody(context);
            if (!string.IsNullOrEmpty(body))
                _logger.LogInformation("{@request_body}", JsonConvert.DeserializeObject(body));
        }

        protected override async Task HandleResponse(HttpContext context)
        {
            var body = await ReadResponseBody(context);
            if (!string.IsNullOrEmpty(body))
                _logger.LogInformation("{@response_body}", JsonConvert.DeserializeObject(body));
        }
    }

    public static class RequestTrackerMiddlewareExtension
    {
        public static IApplicationBuilder UseRequestTrackerMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestTrackerMiddleware>();
        }
    }
}