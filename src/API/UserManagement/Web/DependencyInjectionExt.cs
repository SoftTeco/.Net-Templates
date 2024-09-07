using UserManagement.Application.Data;
using UserManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace UserManagement.Web
{
    public static class DependencyInjectionExt
    {
        public static IServiceCollection AddUserManagementServices(this IServiceCollection services,
         Action<UserManagementOptions> userManagementOptionsAction)
        {
            var userManagementOptions = new UserManagementOptions();
            userManagementOptionsAction(userManagementOptions);

            services.AddDbContext<UsersDbContext>(options =>
            options.UseSqlServer(userManagementOptions.ConnectionString,
                builder => builder.MigrationsAssembly(typeof(UsersDbContext).Assembly.FullName)));
            services.AddScoped<IUsersDbContext>(provider => provider.GetRequiredService<UsersDbContext>());
            return services;
        }
    }

    public sealed class UserManagementOptions
    {
        public string ConnectionString { get; private set; } = "";

        public void UseConnectionString(string connectionString){
            ConnectionString = connectionString;
        }
    }
}