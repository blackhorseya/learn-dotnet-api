using System.Collections.Generic;

namespace Doggy.Learning.Infrastructure.Helpers
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public List<string> RequiredHeaders { get; set; }
        public Authentication Authentication { get; set; } = new Authentication();
    }

    public class Authentication
    {
        public bool Enabled { get; set; } = false;
        public string Secret { get; set; }
        public int Expired { get; set; } = 2;
    }
}