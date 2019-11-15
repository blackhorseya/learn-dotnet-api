namespace Doggy.Learning.Auth.Domain.Entities
{
    public class RoleModuleMap
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        
        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }
    }
}