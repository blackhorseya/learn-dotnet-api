using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Doggy.Extensions.Configuration.Request
{
    public static class Extensions
    {
        public static List<string> TryGetRequiredHeaders(this IConfiguration configuration)
        {
            return configuration.GetSection(Constants.RequiredHeaders).Get<string[]>().ToList();
        }
    }
}