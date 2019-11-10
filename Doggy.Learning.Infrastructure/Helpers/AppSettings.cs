using System.Collections.Generic;

namespace Doggy.Learning.Infrastructure.Helpers
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string Secret { get; set; }
        
        public List<string> RequiredHeaders { get; set; }
    }
}