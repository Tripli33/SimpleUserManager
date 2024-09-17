using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleUserManager.DTOs;
using SimpleUserManager.Models;
using SimpleUserManager.Repositories;

namespace SimpleUserManager.Services;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task BlockAsync(IEnumerable<int> ids)
    {
        var users = await GetAllByIds(ids);

        foreach (var user in users)
        {
            user.UserStatus = UserStatus.Blocked;
            await userRepository.UpdateAsync(user);
        }
    }

    public async Task DeleteAsync(IEnumerable<int> ids)
    {
        var users = await GetAllByIds(ids);

        foreach (var user in users)
        {
            await userRepository.DeleteAsync(user);
        }
    }

    public IEnumerable<UserManipulationDto> GetAll()
    {
        var users = userRepository.GetAll();
        var usersDto = mapper.Map<IEnumerable<UserManipulationDto>>(users);

        return usersDto;
    }

    public async Task UnblockAsync(IEnumerable<int> ids)
    {
        var users = await GetAllByIds(ids);

        foreach (var user in users)
        {
            user.UserStatus = UserStatus.Active;
            await userRepository.UpdateAsync(user);
        }
    }

    private Task<List<User>> GetAllByIds(IEnumerable<int> ids) =>
        userRepository.GetAll(u => ids.Contains(u.UserId)).ToListAsync();
}
