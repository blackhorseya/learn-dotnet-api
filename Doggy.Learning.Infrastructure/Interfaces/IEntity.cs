namespace Doggy.Learning.Infrastructure.Interfaces
{
    public interface IEntity : ITimestampedEntity
    {
        int Id { get; set; }
    }
}