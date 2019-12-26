using System.Collections.Generic;
using System.Linq;
using Doggy.Learning.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Doggy.Learning.Infrastructure.Filters
{
    public class HeaderFilter : IAuthorizationFilter, IOperationFilter
    {
        private readonly List<string> _headers;

        public HeaderFilter(IOptions<AppSettings> appSettings)
        {
            _headers = appSettings.Value.RequiredHeaders;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;
            foreach (var header in _headers.Where(header => !request.Headers.ContainsKey(header)))
            {
                context.Result = new BadRequestResult();
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