using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestDevCom.Web.Services;

namespace TestDevCom.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("signin")]
        public IActionResult SignIn()
        {
            var redirectUrl = Url.Action(nameof(GoogleResponse));
            var properties = _authService.GetGoogleAuthProperties(redirectUrl!);
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var user = await _authService.GetUserInfoAsync(HttpContext);
            if (user == null)
                return RedirectToAction("Index", "Home");

            // TODO: зберегти користувача або авторизувати локально
            // TempData["UserEmail"] = user.Email;

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync(HttpContext);
            return RedirectToAction("Index", "Home");
        }
    }
}
