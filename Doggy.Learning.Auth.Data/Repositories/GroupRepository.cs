using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Interfaces;
using Doggy.Learning.Infrastructure.Interfaces;

namespace Doggy.Learning.Auth.Data.Repositories
{
    public class GroupRepository : GroupRepositoryBase 
    {
        public GroupRepository(AuthContext context) : base(context)
        {
        }
    }
}