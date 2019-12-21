using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Doggy.Learning.Infrastructure.Middlewares
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
            // enable multiple read request body
            context.Request.EnableBuffering();
            
            await HandleRequest(context);
            await Next(context);
            await HandleResponse(context);
        }

        protected virtual async Task HandleRequest(HttpContext context)
        {
            
        }

        protected virtual async Task HandleResponse(HttpContext context)
        {
            
        }

        public async Task<object> GetRequestBody(HttpContext context)
        {
            string requestContent;
            using (var reader = new StreamReader(context.Request.Body))
            {
                requestContent = await reader.ReadToEndAsync();
                context.Request.Body.Seek(0, SeekOrigin.Begin);
            }

            return JsonConvert.DeserializeObject(requestContent);
        }
    }
}