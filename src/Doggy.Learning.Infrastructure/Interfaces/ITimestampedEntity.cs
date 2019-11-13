using System;

namespace Doggy.Learning.Infrastructure.Interfaces
{
    public interface ITimestampedEntity
    {
        DateTime CreatedAt { get; set; }
        
        DateTime UpdatedAt { get; set; }
    }
}