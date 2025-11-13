using EwkQxObd.Api.Authentication;
using EwkQxObd.Api.Authentication.ObjectModel;
using EwkQxObd.WebApi.Models.IqxApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EwkQxObd.WebApi.Controllers.IqxApi
{
    
    public class IqxApiController : Controller
    {
        private readonly AuthClient _authClient;

        public IqxApiController(AuthClient AuthClient)
        {
            _authClient = AuthClient;
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SendRequest(AuthRequestBody requestBody)
        {

            _authClient.AttachRequestBody(requestBody);

            


            var Result = await _authClient.Authenticate();

            if (Result.LoginSuccess)
            {
                HttpContext.Session.SetString("AccessToken", _authClient.AccessToken);
            }

            return RedirectToAction(nameof(LoginResult), Result);
        }

        [HttpGet]
        public IActionResult LoginResult(LoginResultPageModel Result)
        {
            return View(Result);

        }
    }
}
