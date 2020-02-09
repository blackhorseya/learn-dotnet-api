using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Doggy.Extensions.Middlewares
{
    public abstract class MiddlewareBase
    {
        protected readonly RequestDelegate Next;

        protected MiddlewareBase(RequestDelegate next)
        {
            Next = next;
        }

        public virtual async Task Invoke(HttpContext context)
        {
            // temp original response stream
            // replace the original response stream with a readable and writable stream
            var originalRespStream = context.Response.Body;
            await using var ms = new MemoryStream();
            context.Response.Body = ms;

            try
            {
                // handle request
                await HandleRequest(context);

                await Next(context);

                // handle response
                await HandleResponse(context);
            }
            catch
            {
                // copy fake stream to original response stream
                ms.Position = 0;
                await ms.CopyToAsync(originalRespStream);
                context.Response.Body = originalRespStream;

                throw;
            }
        }

        protected virtual async Task HandleRequest(HttpContext context)
        {
            // enable multiple read request body
            context.Request.EnableBuffering();
        }

        protected virtual async Task HandleResponse(HttpContext context)
        {
        }
        
        protected async Task<string> ReadRequestBody(HttpContext context)
        {
            string bodyText;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                context.Request.Body.Position = 0;
                bodyText = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            return bodyText;
        }

        protected async Task<string> ReadResponseBody(HttpContext context)
        {
            string bodyText;
            using (var reader = new StreamReader(context.Response.Body, Encoding.UTF8, true, 1024, true))
            {
                context.Response.Body.Position = 0;
                bodyText = await reader.ReadToEndAsync();
                context.Response.Body.Position = 0;
            }

            return bodyText;
        }
    }
}