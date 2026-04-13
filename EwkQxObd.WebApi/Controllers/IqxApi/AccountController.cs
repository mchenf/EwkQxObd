using EwkQxObd.Api.Authentication;
using EwkQxObd.Api.Authentication.ObjectModel;
using EwkQxObd.WebApi.Authorization;
using EwkQxObd.WebApi.Data.Encryption;
using EwkQxObd.WebApi.Models.IqxApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace EwkQxObd.WebApi.Controllers.IqxApi
{
    
    public class AccountController : Controller
    {
        private readonly LoginManager _loginManager;
        private readonly ITokenProtector _protector;

        public AccountController(LoginManager loginManager, ITokenProtector protector)
        {
            _loginManager = loginManager;
            _protector = protector;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var tokenResponse = await _loginManager.LoginAsync(username, password);

                if (tokenResponse is null || string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    ModelState.AddModelError("", "Empty response from auth0");
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                var encryptedToken = _protector.Protect(tokenResponse.AccessToken);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.UtcNow.AddSeconds(tokenResponse.ExpiresIn)
                };

                Response.Cookies.Append("Auth0.AccessToken", encryptedToken, cookieOptions);

                return Redirect(nameof(LoginResult));

            }
            catch (UnauthorizedAccessException)
            {
                return Redirect(nameof(LoginResult));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginResult()
        {

            return View(User);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Profile()
        {
            return View(User);
        }
    }
}
