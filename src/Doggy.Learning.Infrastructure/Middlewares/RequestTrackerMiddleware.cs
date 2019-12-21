using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Doggy.Learning.Infrastructure.Middlewares
{
    public class RequestTrackerMiddleware : MiddlewareBase
    {
        public RequestTrackerMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        protected override async Task HandleRequest(HttpContext context)
        {
            // todo: handle request 
            await base.HandleRequest(context);
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