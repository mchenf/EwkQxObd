using EwkQxObd.WebApi.Data.FossApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized();
            }
            var result = await _comBff.GetUserOrgsAsync(accessToken);


            return Ok(result);
        }
    }
}
