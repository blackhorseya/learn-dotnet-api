namespace Doggy.LearnNetCore.Domain.Entities
{
    public class PermissionOperation
    {
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }

        public int OperationId { get; set; }
        public Operation Operation { get; set; }
    }
}