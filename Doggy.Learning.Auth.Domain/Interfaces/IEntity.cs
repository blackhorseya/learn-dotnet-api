using System;

namespace Doggy.Learning.Auth.Domain.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}