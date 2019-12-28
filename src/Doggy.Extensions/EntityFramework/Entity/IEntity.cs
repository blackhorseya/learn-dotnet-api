namespace Doggy.Extensions.EntityFramework.Entity
{
    public interface IEntity : ITimestampedEntity
    {
        int Id { get; set; }
    }
}