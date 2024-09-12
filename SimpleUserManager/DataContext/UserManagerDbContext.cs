using Microsoft.EntityFrameworkCore;
using SimpleUserManager.Models;

namespace SimpleUserManager.DataContext;

public class UserManagerDbContext(DbContextOptions<UserManagerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
