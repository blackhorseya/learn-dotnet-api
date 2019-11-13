namespace Doggy.Learning.Auth.Domain.Entities
{
    public class PermissionOperation
    {
        public int PermissionId { get; set; }
        
        public virtual Permission Permission { get; set; }

        public int OperationId { get; set; }
        
        public virtual Operation Operation { get; set; }
    }
}