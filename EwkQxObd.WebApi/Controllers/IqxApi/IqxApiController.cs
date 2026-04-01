using EwkQxObd.Api.Authentication;
using EwkQxObd.Api.Authentication.ObjectModel;
using EwkQxObd.WebApi.Authorization;
using EwkQxObd.WebApi.Models.IqxApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EwkQxObd.WebApi.Controllers.IqxApi
{
    
    public class IqxApiController : Controller
    {
        private readonly AuthHandler _authClient;

        public IqxApiController(AuthHandler AuthClient)
        {
            _authClient = AuthClient;
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

            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignInAsync("FossApi", User, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(3660)
                });

                return Redirect(nameof(LoginResult));
            }
            return Redirect(nameof(LoginResult));
        }

        [HttpGet]
        public IActionResult LoginResult(LoginResultPageModel Result)
        {
            return View(Result);

        }
    }
}
