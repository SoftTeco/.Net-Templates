using UserManagement.Application.Data;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using System.Reflection;

namespace UserManagement.Infrastructure.Data
{
    public class UsersDbContext : DbContext, IUsersDbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> dbContextOptions): base(dbContextOptions)
        {
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}