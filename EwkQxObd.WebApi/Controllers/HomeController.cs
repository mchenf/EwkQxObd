using EwkQxObd.WebApi.Controllers.ewkiqxobd.api;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers
{
    public class HomeController(EwkIqxObdContext ctx, ILogger<HomeController> lgr) : Controller
    {
        private readonly ILogger<HomeController> _logger = lgr;
        private readonly EwkIqxObdContext _context = ctx;

        public IActionResult Index()
        {
            return View();
        }
    }
}
