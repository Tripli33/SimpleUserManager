using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleUserManager.DTOs;
using SimpleUserManager.Services;
using SimpleUserManager.Services.Helpers;
using SimpleUserManager.Exceptions;
using SimpleUserManager.Models;

namespace SimpleUserManager.Pages
{
    public class LoginModel(IAuthService authService,
        IPrincipalProvider principalProvider) : PageModel
    {
        [BindProperty]
        public UserLoginDto? UserDto { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            User user;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                user = await authService.LoginAsync(UserDto!);
            }
            catch (Exception ex) when 
            (ex is InvalidPasswordException || ex is UserBlockedException || ex is UserNotFoundException)
            {
                ModelState.AddModelError("UserDto.Password", 
                    "Incorrect login or password or user is blocked");
                return Page();
            }

            var principal = principalProvider.GeneratePrincipal(user);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToPage(nameof(Index));
        }
    }
}
