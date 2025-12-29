using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;

namespace TestDevCom.Web.Services
{
    public class AuthService : IAuthService
    {
        public AuthenticationProperties GetGoogleAuthProperties(string redirectUrl)
        {
            return new AuthenticationProperties { RedirectUri = redirectUrl };
        }

        public async Task<UserInfo?> GetUserInfoAsync(HttpContext httpContext)
        {
            var result = await httpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
                return null;

            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            return new UserInfo { Email = email, Name = name };
        }

        public async Task SignOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }

}
