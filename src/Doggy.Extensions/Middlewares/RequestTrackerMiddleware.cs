using System.Text.Json;
using System.Threading.Tasks;
using Doggy.Extensions.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Doggy.Extensions.Middlewares
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

        protected override async Task HandleResponse(HttpContext context)
        {
            var reqBody = await ReadRequestBody(context);
            var respBody = await ReadResponseBody(context);
            if (!string.IsNullOrEmpty(respBody))
                _logger.TraceRequest(("response", JsonConvert.DeserializeObject(respBody)));
            else
            {
                _logger.TraceRequest();
            }
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