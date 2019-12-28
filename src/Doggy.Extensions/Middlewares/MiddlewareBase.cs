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
            await HandleRequest(context);

            // temp original response stream
            var originalRespStream = context.Response.Body;
            using (var ms = new MemoryStream())
            {
                // replace the original response stream with a readable and writable stream
                context.Response.Body = ms;
                await Next(context);

                await HandleResponse(context);

                // copy fake stream to original response stream
                ms.Position = 0;
                await ms.CopyToAsync(originalRespStream);
                context.Response.Body = originalRespStream;
            }
        }

        protected virtual async Task HandleRequest(HttpContext context)
        {
        }

        protected virtual async Task HandleResponse(HttpContext context)
        {
        }

        public async Task<string> ReadRequestBody(HttpContext context)
        {
            // enable multiple read request body
            context.Request.EnableBuffering();

            string bodyText;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                context.Request.Body.Position = 0;
                bodyText = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            return bodyText;
        }

        public async Task<string> ReadResponseBody(HttpContext context)
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