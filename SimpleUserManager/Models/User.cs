namespace SimpleUserManager.Models;

public class User
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public DateTime RegistrationDateTime { get; set; }
    public DateTime LastLoginDateTime { get; set; }
    public UserStatus UserStatus { get; set; }
    public string? Password { get; set; }
}
