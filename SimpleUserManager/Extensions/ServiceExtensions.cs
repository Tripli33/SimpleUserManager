using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SimpleUserManager.DataContext;
using SimpleUserManager.Repositories;
using SimpleUserManager.Services;
using SimpleUserManager.Services.Helpers;
using System.Reflection;

namespace SimpleUserManager.Extensions;

public static class ServiceExtensions
{
    public static void AddUserManagerDbContext(this IServiceCollection serviceCollection, 
        string? connectionStringName)
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionStringName);

        var connectionString = Environment.GetEnvironmentVariable(connectionStringName);

        serviceCollection.AddDbContext<UserManagerDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }

    public static void AddAutoMapper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    public static void ConfigureAuthentication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "TastyCookie";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.LoginPath = "/Login";
            });
    }

    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
    }

    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPasswordHasher, PasswordHasher>();
        serviceCollection.AddScoped<IPrincipalProvider, PrincipalProvider>();
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<IUserService, UserService>();
    }
}
