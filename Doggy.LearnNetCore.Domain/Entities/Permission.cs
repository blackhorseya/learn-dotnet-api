using System.Collections.Generic;

namespace Doggy.LearnNetCore.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Role> Roles { get; set; }

        public List<Module> Modules { get; set; }

        public List<Operation> Operations { get; set; }
    }
}