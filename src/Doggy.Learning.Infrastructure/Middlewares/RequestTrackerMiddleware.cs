using System.Threading.Tasks;
using Doggy.Learning.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Doggy.Learning.Infrastructure.Middlewares
{
    public class RequestTrackerMiddleware : MiddlewareBase
    {
        private readonly ILogWrapper _logger;

        public RequestTrackerMiddleware(ILogWrapper logger, RequestDelegate next)
            : base(next)
        {
            _logger = logger;
            _logger = logger;
        }

        protected override async Task HandleRequest(HttpContext context)
        {
            var body = await ReadRequestBody(context);
            if (!string.IsNullOrEmpty(body))
                // _logger.TraceRequestWithMessage("{@request_body}", JsonConvert.DeserializeObject(body));
                _logger.TraceRequestWithMessage("test trace request");
        }

        protected override async Task HandleResponse(HttpContext context)
        {
            var body = await ReadResponseBody(context);
            if (!string.IsNullOrEmpty(body))
                // _logger.LogInformation("{@response_body}", JsonConvert.DeserializeObject(body));
                _logger.TraceRequestWithMessage("test trace response");
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