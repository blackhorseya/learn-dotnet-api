using System;
using Doggy.Learning.Auth.Data;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class GroupRole
    {
        public int GroupId { get; set; }
        
        public Group Group { get; set; }
        
        public int RoleId { get; set; }
        
        public Role Role { get; set; }
    }
}