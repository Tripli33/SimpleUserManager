using SimpleUserManager.DTOs;
using SimpleUserManager.Models;

namespace SimpleUserManager.Services;

public interface IAuthService
{
    Task<User> LoginAsync(UserLoginDto userDto);
    Task<User> RegisterAsync(UserRegistrationDto userDto);
}
