using System.Collections.Generic;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }

        public string Name { get; set; }

        public List<GroupRole> GroupRoles { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
    }
}