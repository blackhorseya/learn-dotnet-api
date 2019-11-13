using Doggy.Learning.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Doggy.Learning.WebService.Design
{
    public class AuthContextFactory : IDesignTimeDbContextFactory<AuthContext>
    {
        public AuthContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthContext>();
            optionsBuilder.UseMySql("Server=localhost;Database=accountDb;User=root;Password=1234jk;");

            return new AuthContext(optionsBuilder.Options);
        }
    }
}