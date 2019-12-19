using System.Collections.Generic;

namespace Doggy.Learning.Infrastructure.Helpers
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public List<string> RequiredHeaders { get; set; }
        public Authentication Authentication { get; set; } = new Authentication();
    }

    public class AppInfo
    {
        public string Name { get; set; } = "learn-dotnet";
        public string Version { get; set; } = "v1";
    }

    public class Authentication
    {
        public bool Enabled { get; set; } = true;
        public string Secret { get; set; }
        public int Expired { get; set; } = 2;
    }
}