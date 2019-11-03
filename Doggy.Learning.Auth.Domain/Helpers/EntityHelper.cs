using Doggy.Learning.Auth.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doggy.Learning.Auth.Domain.Helpers
{
    public static class EntityHelper
    {
        public static EntityTypeBuilder<TEntity> UseTimestampedProperty<TEntity>(this EntityTypeBuilder<TEntity> entity)
            where TEntity : class, ITimestampedEntity
        {
            entity.Property(d => d.CreatedAt).ValueGeneratedOnAdd();
            entity.Property(d => d.UpdatedAt).ValueGeneratedOnAddOrUpdate();

            entity.Property(d => d.CreatedAt).Metadata.BeforeSaveBehavior = PropertySaveBehavior.Ignore;
            entity.Property(d => d.CreatedAt).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            entity.Property(d => d.UpdatedAt).Metadata.BeforeSaveBehavior = PropertySaveBehavior.Ignore;
            entity.Property(d => d.UpdatedAt).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            return entity;
        }
    }
}