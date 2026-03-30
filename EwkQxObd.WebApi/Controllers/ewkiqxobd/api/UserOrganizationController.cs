using EwkQxObd.Core.Model.Iqx;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/[controller]")]
    public class UserOrganizationController : Controller
    {
        private readonly ILogger<UserOrganizationController> _logger;
        private readonly EwkIqxObdContext _context;

        public UserOrganizationController(ILogger<UserOrganizationController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<UserOrganization> results = await _context.UserOrganization.ToListAsync();


            return Ok(new { ContentType = "application/json", Data = results });
        }
    }
}
