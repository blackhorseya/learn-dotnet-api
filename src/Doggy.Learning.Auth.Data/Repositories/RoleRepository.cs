using System.Threading.Tasks;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Interfaces;
using Doggy.Learning.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Doggy.Learning.Auth.Data.Repositories
{
    public class RoleRepository : RoleRepositoryBase
    {
        public RoleRepository(AuthContext context) : base(context)
        {
        }

        public override async Task<Role> FindByNameAsync(string name)
        {
            return await _context.Roles.FirstAsync(r => r.Name == name);
        }
    }
}