using Doggy.Learning.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class AuthContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Module> Modules { get; set; }
        
        public DbSet<Service> Services { get; set; }

        public AuthContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().UseTimestampedProperty();
            modelBuilder.Entity<Role>().UseTimestampedProperty();
            modelBuilder.Entity<Module>().UseTimestampedProperty();
            modelBuilder.Entity<Service>().UseTimestampedProperty();
            
            modelBuilder.Entity<RoleModuleMap>().HasKey(map => new {map.RoleId, map.ModuleId});
            modelBuilder.Entity<RoleServiceMap>().HasKey(map => new {map.RoleId, map.ServiceId});
            modelBuilder.Entity<GroupRoleMap>().HasKey(gr => new {gr.RoleId, GroupId = gr.GroupId});
        }
    }
}