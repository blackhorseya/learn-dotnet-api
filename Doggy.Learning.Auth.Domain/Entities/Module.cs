using System.Collections.Generic;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class Module
    {
        public int ModuleId { get; set; }
        
        public string Name { get; set; }
        
        public List<ModulePermission> ModulePermissions { get; set; }
    }
}