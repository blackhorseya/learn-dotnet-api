using System;
using System.Collections.Generic;
using Doggy.Learning.Auth.Domain.Entities;

namespace Doggy.Learning.Auth.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Role> Roles { get; set; }
        public string Token { get; set; }

        public string GetRolesString()
        {
            return string.Join(',', Roles);
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