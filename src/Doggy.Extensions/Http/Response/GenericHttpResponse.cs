using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Doggy.Extensions.Http.Response
{
    public class GenericHttpResponse
    {
        public int Code { get; set; } = StatusCodes.Status500InternalServerError;
        public bool Ok { get; set; } = false;
        public object Data { get; set; } = new {ErrorMessage = "Unknown error"};

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
            });
        }
    }
}