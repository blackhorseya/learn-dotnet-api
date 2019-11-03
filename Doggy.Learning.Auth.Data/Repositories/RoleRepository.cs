using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Doggy.Learning.Auth.Domain.Entities;
using Doggy.Learning.Auth.Domain.Interfaces;

namespace Doggy.Learning.Auth.Data.Repositories
{
    public class RoleRepository : CrudRepository<Role, AuthContext>
    {
        public RoleRepository(AuthContext context) : base(context)
        {
        }
    }
}