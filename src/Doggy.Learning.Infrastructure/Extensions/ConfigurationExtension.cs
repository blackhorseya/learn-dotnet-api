using System;
using Doggy.Learning.Infrastructure.Constants;
using Microsoft.Extensions.Configuration;

namespace Doggy.Learning.Infrastructure.Extensions
{
    public static class ConfigurationExtension
    {
        public static string GetAppInfoDisplayName(this IConfiguration configuration)
        {
            var appInfoDisplayName = configuration.TryGetAppInfoDisplayName();
            if (string.IsNullOrEmpty(appInfoDisplayName))
                throw new ArgumentNullException(nameof(AppSettingsConstants.AppInfoDisplayName));

            return appInfoDisplayName;
        }

        public static string TryGetAppInfoDisplayName(this IConfiguration configuration)
        {
            return configuration.GetValue(AppSettingsConstants.AppInfoDisplayName, string.Empty);
        }

        public static string TryGetVersion(this IConfiguration configuration)
        {
            return configuration.GetValue(AppSettingsConstants.AppInfoVersion, "v1");
        }

        public static string TryGetVirtualDirectory(this IConfiguration configuration)
        {
            return configuration.GetValue(AppSettingsConstants.VirtualDirectory, "/");
        }

        public static bool TryGetAuthenticationEnabled(this IConfiguration configuration)
        {
            return configuration.GetValue(AppSettingsConstants.AuthenticationEnabled, false);
        }
    }
}