using System.Collections.Generic;
using System.Threading.Tasks;
using Doggy.Extensions.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Doggy.Extensions.Middlewares
{
    public class RequestTrackerMiddleware : MiddlewareBase
    {
        private readonly ILogWrapper _logger;

        public RequestTrackerMiddleware(ILogWrapper logger, RequestDelegate next)
            : base(next)
        {
            _logger = logger;
        }

        protected override async Task HandleResponse(HttpContext context)
        {
            var args = new List<(string, object)>();

            var reqBody = await ReadRequestBody(context);
            var reqObj = RemoveSensitivityWord((JObject) JsonConvert.DeserializeObject(reqBody));
            if (!string.IsNullOrEmpty(reqBody))
                args.Add((Logger.Constants.Properties.RequestBody, reqObj));

            var respBody = await ReadResponseBody(context);
            var resObj = RemoveSensitivityWord((JObject) JsonConvert.DeserializeObject(respBody));
            if (!string.IsNullOrEmpty(respBody))
                args.Add((Logger.Constants.Properties.ResponseBody, resObj));

            _logger.TraceRequest(args.ToArray());
        }

        private static JObject RemoveSensitivityWord(JObject jObject)
        {
            if (jObject == null)
                return null;
            
            if (jObject.ContainsKey("password"))
                jObject.Remove("password");

            return jObject;
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