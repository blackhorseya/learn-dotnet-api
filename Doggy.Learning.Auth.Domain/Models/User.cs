using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Doggy.Learning.Auth.Domain.Entities;

namespace Doggy.Learning.Auth.Domain.Models
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }
        public string Token { get; set; }

        public List<string> GetRolesName()
        {
            return Roles.Select(r => r.Name).ToList();
        }

        public ClaimsIdentity GetClaimsIdentity()
        {
            return new ClaimsIdentity(GetClaims());
        }

        private IEnumerable<Claim> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Name), 
                new Claim(ClaimTypes.Sid, Id.ToString()),
            };
            claims.AddRange(Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            return claims.ToArray();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public static class Extensions
    {
        public static User WithoutPassword(this User user)
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }
    }
}