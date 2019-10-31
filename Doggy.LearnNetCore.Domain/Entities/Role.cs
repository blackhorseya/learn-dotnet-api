using System.Collections.Generic;

namespace Doggy.LearnNetCore.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }

        public string Name { get; set; }

        public List<GroupRole> GroupRoles { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
    }
}