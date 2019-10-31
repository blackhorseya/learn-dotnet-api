using Doggy.LearnNetCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Doggy.LearnNetCore.Domain.Contexts
{
    public class RbacContext : DbContext
    {
        public DbSet<RoleGroup> Groups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public RbacContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}