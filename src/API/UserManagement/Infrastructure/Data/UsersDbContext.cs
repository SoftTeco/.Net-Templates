using UserManagement.Application.Data;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using System.Reflection;
using Infrastructure.Data.Interceptors;

namespace UserManagement.Infrastructure.Data
{
    public class UsersDbContext : DbContext, IUsersDbContext
    {
        private readonly EntitySaveChangesInterceptor _entitySaveChangesInterceptor;

        public UsersDbContext(DbContextOptions<UsersDbContext> dbContextOptions, 
            EntitySaveChangesInterceptor entitySaveChangesInterceptor) : base(dbContextOptions)
        {
            _entitySaveChangesInterceptor = entitySaveChangesInterceptor;
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.AddInterceptors(_entitySaveChangesInterceptor);
    }
}