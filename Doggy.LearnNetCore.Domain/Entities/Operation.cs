using System.Collections.Generic;

namespace Doggy.LearnNetCore.Domain.Entities
{
    public class Operation
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public List<Permission> Permissions { get; set; }
    }
}