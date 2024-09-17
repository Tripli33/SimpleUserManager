using Microsoft.AspNetCore.Authentication.Cookies;
using SimpleUserManager.Models;
using System.Security.Claims;

namespace SimpleUserManager.Services.Helpers;

public class PrincipalProvider : IPrincipalProvider
{
    public ClaimsPrincipal GeneratePrincipal(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        return principal;
    }
}
