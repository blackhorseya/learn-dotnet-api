namespace Doggy.Learning.Auth.Domain.Entities
{
    public class RoleServiceMap
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}