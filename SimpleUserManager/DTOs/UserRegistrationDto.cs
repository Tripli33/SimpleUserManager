using System.ComponentModel.DataAnnotations;

namespace SimpleUserManager.DTOs;

public class UserRegistrationDto
{
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string? UserName { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}
