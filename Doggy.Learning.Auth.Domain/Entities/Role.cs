using System;
using System.Collections.Generic;
using Doggy.Learning.Auth.Data;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class Role : IEntity
    {
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string Name { get; set; }

        public List<GroupRole> GroupRoles { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
    }
}