using SimpleUserManager.Models;

namespace SimpleUserManager.DTOs;

public class UserManipulationDto
{
    public int UserId { get; set; } 
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public DateTime LastLoginDateTime { get; set; }
    public UserStatus UserStatus { get; set; }
}
