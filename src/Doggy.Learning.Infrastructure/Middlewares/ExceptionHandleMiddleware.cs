using System;
using System.Threading.Tasks;
using Doggy.Learning.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Doggy.Learning.Infrastructure.Middlewares
{
    public class ExceptionHandleMiddleware : MiddlewareBase
    {
        private readonly ILogWrapper _logger;

        public ExceptionHandleMiddleware(ILogWrapper logger, RequestDelegate next)
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
                // _logger.Exception(ex, ex.ToString());
                _logger.Exception(ex);
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