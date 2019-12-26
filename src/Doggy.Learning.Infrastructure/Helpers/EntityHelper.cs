using Doggy.Learning.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doggy.Learning.Infrastructure.Helpers
{
    public static class EntityHelper
    {
        public static EntityTypeBuilder<TEntity> UseTimestampedProperty<TEntity>(this EntityTypeBuilder<TEntity> entity)
            where TEntity : class, ITimestampedEntity
        {
            entity.Property(d => d.CreatedAt).ValueGeneratedOnAdd();
            entity.Property(d => d.UpdatedAt).ValueGeneratedOnAddOrUpdate();

            entity.Property(d => d.CreatedAt).Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            entity.Property(d => d.CreatedAt).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            entity.Property(d => d.UpdatedAt).Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            entity.Property(d => d.UpdatedAt).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            return entity;
        }
    }
}