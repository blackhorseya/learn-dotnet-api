using System.Collections.Generic;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        
        public string Name { get; set; }
        
        public List<GroupRole> GroupRoles { get; set; }
    }
}