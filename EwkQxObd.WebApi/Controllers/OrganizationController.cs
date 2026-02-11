using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers
{
    [Route("[controller]")]
    public class OrganizationController(
        EwkIqxObdContext ctx, 
        ILogger<OrganizationController> logger
        ) : Controller
    {
        private readonly EwkIqxObdContext _context = ctx;
        private readonly ILogger<OrganizationController> _logger = logger;


        [Route("")]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orgs = await _context.Vorlks.ToListAsync();
            return View(orgs);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> UnkownOnly()
        {
            var orgs = await _context.Vorlks.
                Where(vo => vo.NetworkId != null)
                .ToListAsync();
            return View(nameof(Index), orgs);
        }
    }
}
