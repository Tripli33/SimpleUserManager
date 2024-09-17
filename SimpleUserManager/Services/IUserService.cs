using SimpleUserManager.DTOs;

namespace SimpleUserManager.Services;

public interface IUserService
{
    IEnumerable<UserManipulationDto> GetAll();
    Task DeleteAsync(IEnumerable<int> ids);
    Task BlockAsync(IEnumerable<int> ids);
    Task UnblockAsync(IEnumerable<int> ids);
}
