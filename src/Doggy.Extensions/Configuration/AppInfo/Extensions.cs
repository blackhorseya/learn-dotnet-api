using System;
using Microsoft.Extensions.Configuration;

namespace Doggy.Extensions.Configuration.AppInfo
{
    public static class Extensions
    {
        public static string GetAppInfoDisplayName(this IConfiguration configuration)
        {
            var appInfoDisplayName = configuration.TryGetAppInfoDisplayName();
            if (string.IsNullOrEmpty(appInfoDisplayName))
                throw new ArgumentNullException(nameof(Constants.AppInfoDisplayName));

            return appInfoDisplayName;
        }

        public static string TryGetAppInfoDisplayName(this IConfiguration configuration)
        {
            return configuration.GetValue(Constants.AppInfoDisplayName, string.Empty);
        }

        public static string TryGetVersion(this IConfiguration configuration)
        {
            return configuration.GetValue(Constants.AppInfoVersion, "v1");
        }
    }
}