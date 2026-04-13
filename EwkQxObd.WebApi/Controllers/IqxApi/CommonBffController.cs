using EwkQxObd.Core.Model.Iqx;
using EwkQxObd.WebApi.Authorization;
using EwkQxObd.WebApi.Data.Encryption;
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
        private readonly ITokenProtector _protector;
        public CommonBffController(CommonBFF commonBFF, ILogger<CommonBffController> logger, ITokenProtector protector)
        {
            _comBff = commonBFF;
            _logger = logger;
            _protector = protector;
        }

        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> Org()
        {
            var accessToken = HttpContext.GetAccessToken(_protector);
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
