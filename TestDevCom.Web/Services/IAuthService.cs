using Microsoft.AspNetCore.Authentication;

namespace TestDevCom.Web.Services
{
    public interface IAuthService
    {
        AuthenticationProperties GetGoogleAuthProperties(string redirectUrl);
        Task<UserInfo?> GetUserInfoAsync(HttpContext httpContext);
        Task SignOutAsync(HttpContext httpContext);
    }

    public class UserInfo
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
    }

}
