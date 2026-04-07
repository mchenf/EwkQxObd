using EwkQxObd.Api.Authentication;
using EwkQxObd.Api.Authentication.ObjectModel;
using EwkQxObd.WebApi.Authorization;
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
    
    public class IqxApiController : Controller
    {
        private readonly LoginManager _loginManager;

        public IqxApiController(LoginManager loginManager)
        {
            _loginManager = loginManager;
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

                HttpContext.Session.SetString("AccessToken", tokenResponse.AccessToken);
                //TODO: Extend expire time or refresh token, etc.

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
        public IActionResult Account()
        {
            return View(User);
        }
    }
}
