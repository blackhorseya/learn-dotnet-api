using Microsoft.Extensions.DependencyInjection;

namespace Doggy.Extensions.Logger
{
    public static class LogWrapperExtensions
    {
        public static IServiceCollection AddLogWrapper(this IServiceCollection services)
        {
            return services.AddSingleton<ILogWrapper, NLogWrapper>();
        }
    }
}