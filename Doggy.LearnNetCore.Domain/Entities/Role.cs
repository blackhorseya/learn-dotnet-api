using System.Collections.Generic;

namespace Doggy.LearnNetCore.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public List<RoleGroup> Groups { get; set; }
        
        public List<Permission> Permissions { get; set; }
    }
}