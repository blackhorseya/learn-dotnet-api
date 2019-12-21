using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Doggy.Learning.Infrastructure.Middlewares
{
    public class ExceptionHandleMiddleware : MiddlewareBase
    {
        public ExceptionHandleMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        public override async Task Invoke(HttpContext context)
        {
            try
            {
                await base.Invoke(context);
            }
            catch (Exception ex)
            {
                // todo: handle exception
                Console.WriteLine(ex);
            }
        }
    }

    public static class ExceptionHandleMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandleMiddleware>();
            return app;
        }
    }
}