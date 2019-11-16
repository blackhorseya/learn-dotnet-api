using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Interfaces;

namespace Doggy.Learning.Auth.Data.Repositories
{
    public class ModuleRepository : ModuleRepositoryBase
    {
        public ModuleRepository(AuthContext context) : base(context)
        {
        }
    }
}