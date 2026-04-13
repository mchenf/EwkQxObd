using EwkQxObd.Core.Model.Iqx;
using EwkQxObd.WebApi.Data.FossApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EwkQxObd.WebApi.Controllers.IqxApi
{
    [Route("[controller]")]
    public class CommonBffController : Controller
    {
        private readonly CommonBFF _comBff;
        private readonly ILogger<CommonBffController> _logger;
        public CommonBffController(CommonBFF commonBFF, ILogger<CommonBffController> logger)
        {
            _comBff = commonBFF;
            _logger = logger;
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> Org()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized();
            }
            var result = await _comBff.GetUserOrgsAsync(accessToken);
            var options = new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
            var orgs = JsonSerializer.Deserialize<List<Organization>>(result, options);

            return View(orgs);
        }
    }
}
