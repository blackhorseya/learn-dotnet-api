using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Doggy.Learning.Auth.Data.Repositories;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Interfaces;
using Doggy.Learning.Auth.Domain.Models;
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

        public UserService(IOptions<AppSettings> authSettings, RoleRepositoryBase roleRepo, GroupRepositoryBase groupRepo)
        {
            _appSettings = authSettings.Value;
            _roleRepo = roleRepo;
            _groupRepo = groupRepo;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));

            // todo: verify username and password

            if (!int.TryParse(username, out var id))
                return null;

            var user = await FindByIdAsync(id);
            if (user == null) throw new ArgumentNullException(nameof(user));

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.Role, user.GetRolesString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public async Task<User> FindByIdAsync(int id)
        {
            Group group = await _groupRepo.GetAsync(id);
            if (group == null) throw new ArgumentNullException(nameof(group));

            return new User
            {
                Id = group.Id,
                Name = group.Name,
                Roles = group.Roles,
            };
        }

        public async Task<List<User>> FindAllAsync()
        {
            // todo: implement it 
            return null;
        }

        public async Task<User> CreateUserAsync(Group group)
        {
            var result = await _groupRepo.AddAsync(group);
            return new User
            {
                Id = result.Id,
                Name = result.Name,
            };
        }
    }
}