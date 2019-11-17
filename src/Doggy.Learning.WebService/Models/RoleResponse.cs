using System.Collections.Generic;

namespace Doggy.Learning.WebService.Models
{
    public class RoleResponse
    {
        public string Name { get; set; }
        public List<string> Services { get; set; }
        public List<string> Modules { get; set; }
    }
}