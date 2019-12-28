using Microsoft.Extensions.DependencyInjection;

namespace Doggy.Extensions.Jwt
{
    public static class Extensions
    {
        public static IServiceCollection AddJwtHelpers(this IServiceCollection services)
        {
            return services.AddSingleton<Helpers>();
        }
    }
}