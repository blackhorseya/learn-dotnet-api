using Doggy.Extensions.EntityFramework.Repository;
using Doggy.Learning.Auth.Domain.Entities;

namespace Doggy.Learning.Auth.Domain.Interfaces
{
    public abstract class ModuleRepositoryBase : CrudRepository<Module, AuthContext>
    {
        protected ModuleRepositoryBase(AuthContext context) : base(context)
        {
        }
    }
}