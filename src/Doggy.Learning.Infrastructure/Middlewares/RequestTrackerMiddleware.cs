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
            await base.HandleRequest(context);
            var reqBody = await GetRequestBody(context);
            _logger.LogInformation("{@request_body}", JsonConvert.DeserializeObject(reqBody));
        }

        protected override async Task HandleResponse(HttpContext context)
        {
            // todo: handle response
            await base.HandleResponse(context);
        }
    }

    public static class RequestTrackerMiddlewareExtension
    {
        public static IApplicationBuilder UseRequestTrackerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestTrackerMiddleware>();
            return app;
        }
    }
}