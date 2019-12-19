using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Interfaces;
using Doggy.Learning.Infrastructure.Helpers;

namespace Doggy.Learning.Auth.Business.Services
{
    public class UserService : IUserService
    {
        private readonly GroupRepositoryBase _groupRepo;
        private readonly JwtHelper _jwtHelper;

        public UserService(GroupRepositoryBase groupRepo, JwtHelper jwtHelper)
        {
            _groupRepo = groupRepo;
            _jwtHelper = jwtHelper;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));

            // todo: verify username and password
            var group = await FindByNameAsync(username);

            // authentication successful so generate jwt token
            return _jwtHelper.GenerateToken(group.GetClaimsIdentity());
        }

        public async Task<Group> FindByIdAsync(int id)
        {
            var group = await _groupRepo.GetAsync(id);
            if (group == null) throw new ArgumentNullException(nameof(group));

            return group;
        }

        public async Task<Group> FindByNameAsync(string name)
        {
            var group = await _groupRepo.FindByNameAsync(name);
            if (group == null) throw new ArgumentNullException(nameof(group));

            return group;
        }

        public async Task<List<Group>> FindAllAsync()
        {
            return await _groupRepo.GetAllAsync();
        }
    }
}