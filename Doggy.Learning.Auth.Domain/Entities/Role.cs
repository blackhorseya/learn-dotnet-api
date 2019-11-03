using System;
using System.Collections.Generic;
using Doggy.Learning.Auth.Domain.Interfaces;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class Role : IEntity
    {
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string Name { get; set; }

        public List<GroupRole> GroupRoles { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
    }
}