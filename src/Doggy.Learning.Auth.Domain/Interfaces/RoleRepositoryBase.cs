using System.Threading.Tasks;
using Doggy.Extensions.EntityFramework.Repository;
using Doggy.Learning.Auth.Domain.Entities;

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