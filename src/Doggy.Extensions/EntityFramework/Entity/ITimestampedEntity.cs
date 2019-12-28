using System;

namespace Doggy.Extensions.EntityFramework.Entity
{
    public interface ITimestampedEntity
    {
        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}