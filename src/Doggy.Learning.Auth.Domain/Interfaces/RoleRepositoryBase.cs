using System.Threading.Tasks;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Infrastructure.Interfaces;

namespace Doggy.Learning.Auth.Domain.Interfaces
{
    public abstract class RoleRepositoryBase : CrudRepository<Role, AuthContext>
    {
        protected RoleRepositoryBase(AuthContext context) : base(context)
        {
        }

        public abstract Task<Role> FindByNameAsync(string name);
    }
}