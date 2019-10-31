using System.Collections.Generic;

namespace Doggy.LearnNetCore.Domain.Entities
{
    public class RoleGroup
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public List<Role> Roles { get; set; }
    }
}