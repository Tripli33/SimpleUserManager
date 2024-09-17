using Microsoft.EntityFrameworkCore;
using SimpleUserManager.Models;

namespace SimpleUserManager.DataContext;

public class UserManagerDbContext(DbContextOptions<UserManagerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
    }
}
