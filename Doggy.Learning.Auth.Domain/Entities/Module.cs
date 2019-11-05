using System;
using System.Collections.Generic;
using Doggy.Learning.Infrastructure.Interfaces;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class Module : IEntity
    {
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string Name { get; set; }
        
        public virtual List<ModulePermission> ModulePermissions { get; set; }
    }
}