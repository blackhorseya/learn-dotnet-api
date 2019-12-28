using Doggy.Extensions.EntityFramework.Repository;
using Doggy.Learning.Auth.Domain.Entities;

namespace Doggy.Learning.Auth.Domain.Interfaces
{
    public abstract class ServiceRepositoryBase : CrudRepository<Service, AuthContext>
    {
        protected ServiceRepositoryBase(AuthContext context) : base(context)
        {
        }
    }
}