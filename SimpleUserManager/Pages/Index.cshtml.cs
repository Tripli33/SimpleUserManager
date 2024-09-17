using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleUserManager.DTOs;
using SimpleUserManager.Services;
using System.Security.Claims;

namespace SimpleUserManager.Pages;

[Authorize]
public class IndexModel(IUserService userService) : PageModel
{
    [BindProperty]
    public List<int> AreChecked { get; set; } = new List<int>();
    public IEnumerable<UserManipulationDto> Users { get; } = userService.GetAll();
    public void OnGet()
    {
    }
    public async Task<IActionResult> OnGetLogoutAsync()
    {
        await HttpContext.SignOutAsync();

        return RedirectToPage("Login");
    }

    public async Task<IActionResult> OnPostBlockAsync() 
    {
        await LogoutIfPicked();
        await userService.BlockAsync(AreChecked);

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUnblockAsync()
    {
        await userService.UnblockAsync(AreChecked);

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        await LogoutIfPicked();
        await userService.DeleteAsync(AreChecked);

        return RedirectToPage();
    }

    private async Task LogoutIfPicked()
    {
        if (AreChecked.Contains(GetUserIdFromClaims()))
        {
            await HttpContext.SignOutAsync();
        }
    }

    private int GetUserIdFromClaims()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return Convert.ToInt32(userId);
    }

}
