using System.Threading.Tasks;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Doggy.Learning.Auth.Data.Repositories
{
    public class GroupRepository : GroupRepositoryBase
    {
        public GroupRepository(AuthContext context) : base(context)
        {
        }

        public override async Task<Group> FindByNameAsync(string name)
        {
            return await _context.Groups.FirstOrDefaultAsync(g => g.Name == name);
        }
    }
}