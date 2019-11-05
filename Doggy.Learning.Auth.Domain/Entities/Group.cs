using System;
using System.Collections.Generic;
using System.Linq;
using Doggy.Learning.Infrastructure.Interfaces;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class Group : IEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public string Name { get; set; }

        public List<GroupRole> GroupRoles { get; set; }

        public List<Role> Roles => GroupRoles?.Select(gr => gr.Role).ToList();
    }
}