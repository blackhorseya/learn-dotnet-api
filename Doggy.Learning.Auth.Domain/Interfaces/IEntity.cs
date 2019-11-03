using System;

namespace Doggy.Learning.Auth.Domain.Interfaces
{
    public interface IEntity : ITimestampedEntity
    {
        int Id { get; set; }
    }
}