namespace Doggy.Learning.Auth.Domain.Entities
{
    public class GroupRole
    {
        public int GroupId { get; set; }
        
        public virtual Group Group { get; set; }
        
        public int RoleId { get; set; }
        
        public virtual Role Role { get; set; }
    }
}