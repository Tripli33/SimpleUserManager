using SimpleUserManager.Models;
using System.Linq.Expressions;

namespace SimpleUserManager.Repositories;

public interface IUserRepository
{
    User? GetById(int id);
    User? GetByEmail(string email);
    IEnumerable<User> GetAll();
    IQueryable<User> GetAll(Expression<Func<User, bool>> filter);
    Task CreateAsync(User user);
    Task DeleteAsync(User user);
    Task UpdateAsync(User user);
}
