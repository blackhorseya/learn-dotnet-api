using System;
using System.Collections.Generic;
using Doggy.Extensions.EntityFramework.Entity;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class Module : IEntity
    {
        public string Name { get; set; }

        public virtual List<RoleModuleMap> Roles { get; set; }
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}