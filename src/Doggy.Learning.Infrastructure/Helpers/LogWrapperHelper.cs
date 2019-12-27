using Doggy.Learning.Infrastructure.Entities;
using Doggy.Learning.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Doggy.Learning.Infrastructure.Helpers
{
    public static class LogWrapperHelper
    {
        public static IServiceCollection UseLogWrapper(this IServiceCollection services)
        {
            return services.AddSingleton<ILogWrapper, NLogWrapper>();
        }
    }
}