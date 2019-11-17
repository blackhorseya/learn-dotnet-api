using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Infrastructure.Interfaces;

namespace Doggy.Learning.Auth.Domain.Interfaces
{
    public abstract class ServiceRepositoryBase : CrudRepository<Service, AuthContext>
    {
        protected ServiceRepositoryBase(AuthContext context) : base(context)
        {
        }
    }
}