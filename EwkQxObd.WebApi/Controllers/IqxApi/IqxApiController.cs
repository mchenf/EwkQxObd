using Microsoft.AspNetCore.Mvc;
using EwkQxObd.Api.Authentication;
using EwkQxObd.Api.Authentication.ObjectModel;
using EwkQxObd.WebApi.Models.IqxApi;

namespace EwkQxObd.WebApi.Controllers.IqxApi
{
    
    public class IqxApiController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SendRequest(AuthRequestBody requestBody)
        {


            return RedirectToAction("LoginResult");
        }

        [HttpGet]
        public IActionResult LoginResult()
        {
            return View(new LoginResultPageModel());

        }
    }
}
