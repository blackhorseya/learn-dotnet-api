namespace Doggy.Learning.Auth.Domain.Entities
{
    public class ModulePermission
    {
        public int ModuleId { get; set; }
        
        public Module Module { get; set; }
        
        public int PermissionId { get; set; }
        
        public Permission Permission { get; set; }
    }
}