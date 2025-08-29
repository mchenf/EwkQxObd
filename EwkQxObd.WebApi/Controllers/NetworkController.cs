using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class NetworkController : Controller
    {
        private readonly ILogger<NetworkController> _logger;
        private readonly EwkIqxObdContext _context;


        public NetworkController(ILogger<NetworkController> logger, EwkIqxObdContext ctx)
        {
            _logger = logger;
            _context = ctx;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _context.VwSysnetinstorg
                .ToListAsync();
            if (result != default)
            {
                return View(result);
            }

            return NoContent();
        }



    }
}
