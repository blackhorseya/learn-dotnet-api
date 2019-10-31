using System.Collections.Generic;

namespace Doggy.LearnNetCore.Domain.Entities
{
    public class Module
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public List<Permission> Permissions { get; set; }
    }
}