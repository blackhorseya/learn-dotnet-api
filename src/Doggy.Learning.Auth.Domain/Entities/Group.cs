using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Doggy.Learning.Infrastructure.Constants;
using Doggy.Learning.Infrastructure.Interfaces;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class Group : IEntity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public string Name { get; set; }

        public virtual List<GroupRoleMap> Roles { get; set; }

        public List<Role> GetRoles()
        {
            return Roles?.Where(gr => gr.GroupId == Id).Select(gr => gr.Role).ToList();
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
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            };
            claims.AddRange(Roles.Select(map => new Claim(ClaimTypes.Role, map.Role.Name)));

            return claims.ToArray();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}