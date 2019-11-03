using System;

namespace Doggy.Learning.Auth.Data
{
    public interface IEntity
    {
        int Id { get; set; }

        DateTime CreatedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}