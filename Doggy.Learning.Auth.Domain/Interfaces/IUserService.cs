using System.Collections.Generic;
using System.Threading.Tasks;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Models;

namespace Doggy.Learning.Auth.Domain.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        
        Task<User> FindByIdAsync(int id);
        
        Task<User> FindByNameAsync(string name);
        
        Task<List<User>> FindAllAsync();

        Task<User> CreateUserAsync(Group group);
    }
}