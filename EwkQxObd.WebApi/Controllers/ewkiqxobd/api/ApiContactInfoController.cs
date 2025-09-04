using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/contactinfo")]
    public class ApiContactInfoController : Controller
    {
        private readonly ILogger<ApiContactInfoController> _logger;
        private readonly EwkIqxObdContext _context;

        public ApiContactInfoController(ILogger<ApiContactInfoController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("byemail/{emailAddress}")]
        public async Task<EqoContactInfo?> GetByEmail([FromRoute] string emailAddress)
        {
            string decoded = WebUtility.UrlDecode(emailAddress);
            var Result = await _context.EqoContactInfo
                .Where(a => a.EmailAddress == emailAddress).FirstOrDefaultAsync();

            return Result;
        }

    }
}
