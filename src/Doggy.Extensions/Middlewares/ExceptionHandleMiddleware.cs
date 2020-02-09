using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Doggy.Extensions.Exceptions;
using Doggy.Extensions.Http;
using Doggy.Extensions.Http.Response;
using Doggy.Extensions.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Doggy.Extensions.Middlewares
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
                _logger.Exception(ex);
                await HandlingExceptionAsync(context, ex);
            }
        }

        private async Task HandlingExceptionAsync(HttpContext context, Exception ex)
        {
            var res = new GenericHttpResponse
            {
                Code = StatusCodes.Status500InternalServerError,
                Ok = false,
                Data = new
                {
                    ErrorMessage = "Unknown error",
                }
            };

            if (ex is FaultInfoBase faultInfoBase)
            {
                res.Code = (int) faultInfoBase.HttpStatusCode;
                res.Data = new
                {
                    faultInfoBase.ErrorCode,
                    faultInfoBase.ErrorMessage,
                };
            }

            if (ex is MissingFieldException missingFieldException)
            {
                res.Code = StatusCodes.Status400BadRequest;
                res.Data = new
                {
                    ErrorMassage = missingFieldException.Message,
                };
            }
            
            context.Response.Clear();
            context.Response.StatusCode = res.Code;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            await context.WriteResponseBody(res.ToJson());
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