using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Doggy.Extensions.Http
{
    public static class Extensions
    {
        public static async Task WriteResponseBody(this HttpContext context, string body)
        {
            // temp original response stream
            var originalRespStream = context.Response.Body;
            await using var ms = new MemoryStream();
            
            // replace the original response stream with a readable and writable stream
            context.Response.Body = ms;
            
            await context.Response.WriteAsync(body, Encoding.UTF8);
            
            // copy fake stream to original response stream
            ms.Position = 0;
            await ms.CopyToAsync(originalRespStream);
            context.Response.Body = originalRespStream;
        }
    }
}