using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Interfaces;
using Doggy.Learning.Infrastructure.Interfaces;

namespace Doggy.Learning.Auth.Data.Repositories
{
    public class RoleRepository : RoleRepositoryBase
    {
        public RoleRepository(AuthContext context) : base(context)
        {
        }
    }
}