using System.Collections.Generic;
using System.Threading.Tasks;
using Doggy.Learning.Auth.Domain.Entities;

namespace Doggy.Learning.Auth.Domain.Interfaces
{
    public interface IUserService
    {
        Task<string> Authenticate(string username, string password);

        Task<Group> FindByIdAsync(int id);

        Task<Group> FindByNameAsync(string name);

        Task<List<Group>> FindAllAsync();
    }
}