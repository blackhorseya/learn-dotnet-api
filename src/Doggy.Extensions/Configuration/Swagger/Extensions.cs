using Microsoft.Extensions.Configuration;

namespace Doggy.Extensions.Configuration.Swagger
{
    public static class Extensions
    {
        public static string TryGetVirtualDirectory(this IConfiguration configuration)
        {
            return configuration.GetValue(Constants.VirtualDirectory, "/");
        }
    }
}