using System.Threading.Tasks;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Infrastructure.Interfaces;

namespace Doggy.Learning.Auth.Domain.Interfaces
{
    public abstract class GroupRepositoryBase : CrudRepository<Group, AuthContext>
    {
        protected GroupRepositoryBase(AuthContext context) : base(context)
        {
        }

        public abstract Task<Group> FindByNameAsync(string name);
    }
}