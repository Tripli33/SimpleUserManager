using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimpleUserManager.DTOs;
using SimpleUserManager.Models;
using SimpleUserManager.Services;
using SimpleUserManager.Services.Helpers;

namespace SimpleUserManager.Pages
{
    public class RegisterModel(IAuthService authService, 
        IPrincipalProvider principalProvider) : PageModel
    {
        [BindProperty]
        public UserRegistrationDto? UserDto { get; set; }

        public async Task<IActionResult> OnPostAsync() 
        {
            User user;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                user = await authService.RegisterAsync(UserDto!);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("UserDto.Email", "Email is already exists");
                return Page();
            }

            var principal = principalProvider.GeneratePrincipal(user);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToPage(nameof(Index));
        }
    }
}
