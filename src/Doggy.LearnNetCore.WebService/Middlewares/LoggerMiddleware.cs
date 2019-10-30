using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Doggy.LearnNetCore.WebService.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(state =>
            {
                var ctx = (HttpContext) state;
                ctx.Response.WriteAsync($"{nameof(LoggerMiddleware)} in\r\n");
                return Task.FromResult(0);
            }, context);
            
            await _next(context);

            await context.Response.WriteAsync($"\r\n{nameof(LoggerMiddleware)} out\r\n");
        }
    }
}