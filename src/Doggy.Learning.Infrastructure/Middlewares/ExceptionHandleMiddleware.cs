using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Doggy.Learning.Infrastructure.Middlewares
{
    public class ExceptionHandleMiddleware : MiddlewareBase
    {
        private readonly ILogger<ExceptionHandleMiddleware> _logger;

        public ExceptionHandleMiddleware(ILogger<ExceptionHandleMiddleware> logger, RequestDelegate next)
            : base(next)
        {
            _logger = logger;
        }

        public override async Task Invoke(HttpContext context)
        {
            try
            {
                await base.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
            }
        }
    }

    public static class ExceptionHandleMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandleMiddleware>();
        }
    }
}