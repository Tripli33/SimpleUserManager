using SimpleUserManager.DataContext;
using SimpleUserManager.Models;
using System.Linq.Expressions;

namespace SimpleUserManager.Repositories;

public class UserRepository(UserManagerDbContext context) : IUserRepository
{
    public async Task CreateAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        context.Remove(user);
        await context.SaveChangesAsync();
    }

    public IEnumerable<User> GetAll() => context.Users;

    public IQueryable<User> GetAll(Expression<Func<User, bool>> filter) => context.Users.Where(filter);    

    public User? GetByEmail(string email) => context.Users.SingleOrDefault(u => u.Email == email);

    public User? GetById(int id) => context.Users.SingleOrDefault(u => u.UserId == id);

    public async Task UpdateAsync(User user)
    {
        context.Update(user);
        await context.SaveChangesAsync();
    }
}
