using System.Collections.Generic;
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
            var args = new List<(string, object)>();
            
            var reqBody = await ReadRequestBody(context);
            if (!string.IsNullOrEmpty(reqBody))
                args.Add((Logger.Constants.Properties.RequestBody, JsonConvert.DeserializeObject(reqBody)));
            
            var respBody = await ReadResponseBody(context);
            if (!string.IsNullOrEmpty(respBody))
                args.Add((Logger.Constants.Properties.ResponseBody, JsonConvert.DeserializeObject(respBody)));
            
            _logger.TraceRequest(args.ToArray());
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