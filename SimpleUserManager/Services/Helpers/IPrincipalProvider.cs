using SimpleUserManager.Models;
using System.Security.Claims;

namespace SimpleUserManager.Services.Helpers;

public interface IPrincipalProvider
{
    ClaimsPrincipal GeneratePrincipal(User user);
}
