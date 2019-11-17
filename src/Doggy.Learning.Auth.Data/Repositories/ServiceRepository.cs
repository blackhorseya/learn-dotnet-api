using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Interfaces;

namespace Doggy.Learning.Auth.Data.Repositories
{
    public class ServiceRepository : ServiceRepositoryBase
    {
        public ServiceRepository(AuthContext context) : base(context)
        {
        }
    }
}