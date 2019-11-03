using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Infrastructure.Interfaces;

namespace Doggy.Learning.Auth.Data.Repositories
{
    public class RoleRepository : CrudRepository<Role, AuthContext>
    {
        public RoleRepository(AuthContext context) : base(context)
        {
        }
    }
}