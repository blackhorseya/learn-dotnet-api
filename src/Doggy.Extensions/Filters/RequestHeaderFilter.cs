using System.Collections.Generic;
using System.Linq;
using Doggy.Extensions.Configuration.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Doggy.Extensions.Filters
{
    public class RequestHeaderFilter : IAuthorizationFilter, IOperationFilter
    {
        private readonly List<string> _headers;

        public RequestHeaderFilter(IConfiguration configuration)
        {
            _headers = configuration.TryGetRequiredHeaders();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;
            foreach (var header in _headers.Where(header => !request.Headers.ContainsKey(header)))
            {
                var result = new ObjectResult(new
                {
                    ErrorMessage = $"The [{header}] field is required in request headers",
                });
                result.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = result;
                break;
            }
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            // var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            // var isHeader = filterPipeline.Any(filter => filter.Filter is HeaderFilter);

            foreach (var header in _headers)
                operation.Parameters.Add(new OpenApiParameter
                {
                    In = ParameterLocation.Header,
                    Name = header,
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "string"
                    }
                });
        }
    }
}