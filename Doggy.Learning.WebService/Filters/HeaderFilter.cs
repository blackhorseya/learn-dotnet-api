using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Doggy.Learning.WebService.Filters
{
    public class HeaderFilter : IAuthorizationFilter
    {
        private readonly List<string> _headers;

        public HeaderFilter(List<string> headers)
        {
            _headers = headers;
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
    }
}