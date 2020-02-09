using System;
using Doggy.Extensions.Http.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Doggy.Extensions.Filters
{
    public class FormatResultFilter : Attribute, IAlwaysRunResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!(context.Result is ObjectResult result))
                return;
            
            var res = new GenericHttpResponse()
            {
                Code = result.StatusCode ?? 0,
                Ok = result.StatusCode < StatusCodes.Status400BadRequest,
                Data = result.Value
            };

            result.Value = res;
        }
    }
}