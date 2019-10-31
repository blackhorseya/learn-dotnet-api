using System.Collections.Generic;

namespace Doggy.LearnNetCore.Domain.Entities
{
    public class Permission
    {
        public int PermissionId { get; set; }

        public string Name { get; set; }

        public List<RolePermission> RolePermissions { get; set; }

        public List<ModulePermission> ModulePermissions { get; set; }

        public List<PermissionOperation> PermissionOperations { get; set; }
    }
}