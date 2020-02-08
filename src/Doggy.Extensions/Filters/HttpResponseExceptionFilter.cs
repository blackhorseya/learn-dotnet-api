using Doggy.Extensions.Exceptions;
using Doggy.Extensions.HttpResponse;
using Doggy.Extensions.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Doggy.Extensions.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        private readonly ILogWrapper _logger;

        public HttpResponseExceptionFilter(ILogWrapper logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.Exception(context.Exception);
            var res = new GenericHttpResponse
            {
                Code = StatusCodes.Status500InternalServerError,
                Ok = false,
                Data = new
                {
                    ErrorMessage = "Unknown error",
                }
            };
            if (context.Exception is FaultInfoBase ex)
            {
                res.Code = (int) ex.HttpStatusCode;
                res.Data = new
                {
                    ex.ErrorCode,
                    ex.ErrorMessage,
                };
            }
            
            context.Result = new ObjectResult(res)
            {
                StatusCode = res.Code,
            };
            context.ExceptionHandled = true;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public int Order { get; } = int.MaxValue - 10;
    }
}