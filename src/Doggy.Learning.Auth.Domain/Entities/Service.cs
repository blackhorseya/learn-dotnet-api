using System;
using System.Collections.Generic;
using Doggy.Learning.Infrastructure.Interfaces;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class Service : IEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<RoleServiceMap> Roles { get; set; }
    }
}