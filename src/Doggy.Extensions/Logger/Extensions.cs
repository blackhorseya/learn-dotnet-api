using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Doggy.Extensions.Logger
{
    public static class Extensions
    {
        public static IServiceCollection AddLogWrapper(this IServiceCollection services)
        {
            return services.AddSingleton<ILogWrapper, NLogWrapper>();
        }

        public static (string, object)[] AddCategoryAndMessage(this (string, object)[] args, string category,
            string message = "")
        {
            var lst = args.ToList();
            lst.Add((Constants.Properties.Category, category));
            if (!string.IsNullOrEmpty(message))
                lst.Add((Constants.Properties.Message, message));

            return lst.ToArray();
        }

        public static (string, object[]) ConvertToNLog(this IEnumerable<(string, object)> args)
        {
            var keys = string.Join("", args.Select(x => $"{{@{x.Item1}}}").ToList());
            var values = args.Select(x => x.Item2).ToArray();

            return (keys, values);
        }
    }
}