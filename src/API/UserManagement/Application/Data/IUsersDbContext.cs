using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Data
{
    public interface IUsersDbContext
    {
       public DbSet<User> Users { get; } 

       Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}