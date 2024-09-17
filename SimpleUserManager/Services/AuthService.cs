using AutoMapper;
using SimpleUserManager.DTOs;
using SimpleUserManager.Exceptions;
using SimpleUserManager.Models;
using SimpleUserManager.Repositories;
using SimpleUserManager.Services.Helpers;

namespace SimpleUserManager.Services;

public class AuthService(IUserRepository userRepository, 
    IPasswordHasher passwordHasher, IMapper mapper) : IAuthService
{
    public async Task<User> LoginAsync(UserLoginDto userDto)
    {
        var user = userRepository.GetByEmail(userDto.Email!);

        if (user == null) 
        {
            throw new UserNotFoundException(userDto.Email!);
        }

        if (user.UserStatus == UserStatus.Blocked)
        {
            throw new UserBlockedException(userDto.Email!);
        }

        var isValidPassword = passwordHasher.Verify(userDto.Password!, user.Password!);

        if (!isValidPassword)
        {
            throw new InvalidPasswordException();
        }

        user.LastLoginDateTime = DateTime.Now;
        await userRepository.UpdateAsync(user);

        return user;
    }

    public async Task<User> RegisterAsync(UserRegistrationDto userDto)
    {
        userDto.Password = passwordHasher.Generate(userDto.Password!);

        var user = mapper.Map<User>(userDto);

        user.RegistrationDateTime = DateTime.Now;
        user.LastLoginDateTime = DateTime.Now;
        await userRepository.CreateAsync(user);

        return user;
    }
}