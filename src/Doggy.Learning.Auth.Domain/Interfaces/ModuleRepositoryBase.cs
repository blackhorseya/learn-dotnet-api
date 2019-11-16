using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Infrastructure.Interfaces;

namespace Doggy.Learning.Auth.Domain.Interfaces
{
    public abstract class ModuleRepositoryBase : CrudRepository<Module, AuthContext>
    {
        protected ModuleRepositoryBase(AuthContext context) : base(context)
        {
        }
    }
}