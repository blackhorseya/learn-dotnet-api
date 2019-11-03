using System;

namespace Doggy.Learning.Auth.Domain.Interfaces
{
    public interface ITimestampedEntity
    {
        DateTime CreatedAt { get; set; }
        
        DateTime UpdatedAt { get; set; }
    }
}