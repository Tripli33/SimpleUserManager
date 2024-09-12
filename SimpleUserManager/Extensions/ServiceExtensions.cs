using Microsoft.EntityFrameworkCore;
using SimpleUserManager.DataContext;

namespace SimpleUserManager.Extensions;

public static class ServiceExtensions
{
    public static void AddUserManagerDbContext(this IServiceCollection serviceCollection, 
        string? connectionStringName)
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionStringName);

        var configuration = serviceCollection.BuildServiceProvider().GetService<IConfiguration>();
        var connectionString = configuration!.GetConnectionString(connectionStringName);

        serviceCollection.AddDbContext<UserManagerDbContext>(options =>
        {
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36)));
        });
    }
}
