using EwkQxObd.WebApi.Controllers.ewkiqxobd.api;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EwkIqxObdContext _context;
        public HomeController(EwkIqxObdContext ctx, ILogger<HomeController> lgr)
        {
            _context = ctx;
            _logger = lgr;

        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.VwSysnetinstorg.ToListAsync());
        }
    }
}
