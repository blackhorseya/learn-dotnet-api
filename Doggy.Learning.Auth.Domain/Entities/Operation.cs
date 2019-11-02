using System.Collections.Generic;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class Operation
    {
        public int OperationId { get; set; }
        
        public string Name { get; set; }
        
        public List<PermissionOperation> PermissionOperations { get; set; }
    }
}