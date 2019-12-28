using Microsoft.Extensions.Configuration;

namespace Doggy.Extensions.Configuration.Authentication
{
    public static class Extensions
    {
        public static bool TryGetAuthenticationEnabled(this IConfiguration configuration)
        {
            return configuration.GetValue(Constants.Enabled, false);
        }

        public static string TryGetAuthenticationSecret(this IConfiguration configuration)
        {
            return configuration.GetValue(Constants.Secret, string.Empty);
        }

        public static int TryGetAuthenticationExpired(this IConfiguration configuration)
        {
            return configuration.GetValue(Constants.Expired, 2);
        }
    }
}