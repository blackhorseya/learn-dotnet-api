using Doggy.Learning.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Doggy.Learning.Auth.Domain.Entities
{
    public class AuthContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public AuthContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().UseTimestampedProperty();
            modelBuilder.Entity<Role>().UseTimestampedProperty();
            modelBuilder.Entity<Permission>().UseTimestampedProperty();
            modelBuilder.Entity<Module>().UseTimestampedProperty();
            modelBuilder.Entity<Operation>().UseTimestampedProperty();
            
            modelBuilder.Entity<ModulePermission>().HasKey(mp => new {mp.ModuleId, mp.PermissionId});
            modelBuilder.Entity<ModulePermission>()
                .HasOne(mp => mp.Module)
                .WithMany(m => m.ModulePermissions)
                .HasForeignKey(mp => mp.PermissionId);
            modelBuilder.Entity<ModulePermission>()
                .HasOne(mp => mp.Permission)
                .WithMany(p => p.ModulePermissions)
                .HasForeignKey(mp => mp.ModuleId);

            modelBuilder.Entity<PermissionOperation>().HasKey(po => new {po.OperationId, po.PermissionId});
            modelBuilder.Entity<PermissionOperation>()
                .HasOne(po => po.Operation)
                .WithMany(o => o.PermissionOperations)
                .HasForeignKey(po => po.PermissionId);
            modelBuilder.Entity<PermissionOperation>()
                .HasOne(po => po.Permission)
                .WithMany(p => p.PermissionOperations)
                .HasForeignKey(po => po.OperationId);

            modelBuilder.Entity<RolePermission>().HasKey(rp => new {rp.RoleId, rp.PermissionId});
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<GroupRole>().HasKey(gr => new {gr.RoleId, GroupId = gr.GroupId});
            modelBuilder.Entity<GroupRole>()
                .HasOne(gr => gr.Role)
                .WithMany(r => r.GroupRoles)
                .HasForeignKey(gr => gr.GroupId);
            modelBuilder.Entity<GroupRole>()
                .HasOne(gr => gr.Group)
                .WithMany(g => g.GroupRoles)
                .HasForeignKey(gr => gr.RoleId);
        }
    }
}