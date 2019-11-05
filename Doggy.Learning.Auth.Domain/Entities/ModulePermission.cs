namespace Doggy.Learning.Auth.Domain.Entities
{
    public class ModulePermission
    {
        public int ModuleId { get; set; }

        public virtual Module Module { get; set; }

        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
    }
}