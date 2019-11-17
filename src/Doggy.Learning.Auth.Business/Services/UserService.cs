using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Doggy.Learning.Auth.Data.Repositories;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Interfaces;
using Doggy.Learning.Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Doggy.Learning.Auth.Business.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly RoleRepositoryBase _roleRepo;
        private readonly GroupRepositoryBase _groupRepo;

        public UserService(IOptions<AppSettings> authSettings, RoleRepositoryBase roleRepo,
            GroupRepositoryBase groupRepo)
        {
            _appSettings = authSettings.Value;
            _roleRepo = roleRepo;
            _groupRepo = groupRepo;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));

            // todo: verify username and password
            var group = await FindByNameAsync(username);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = group.GetClaimsIdentity(),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
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