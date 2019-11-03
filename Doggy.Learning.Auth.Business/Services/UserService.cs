using System;
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
        private readonly AuthSettings _authSettings;
        private readonly RoleRepository _roleRepo;

        public UserService(IOptions<AuthSettings> authSettings, RoleRepository roleRepo)
        {
            _authSettings = authSettings.Value;
            _roleRepo = roleRepo;
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
            var key = Encoding.ASCII.GetBytes(_authSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
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
            var role = await _roleRepo.GetAsync(id);
            if (role == null) throw new ArgumentNullException(nameof(role));

            return new User
            {
                Id = role.Id,
                Role = role.Name,
            };
        }
    }
}