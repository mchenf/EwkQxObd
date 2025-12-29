using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers
{
    public class EmailContactController(
        EwkIqxObdContext ctx, ILogger<EmailContactController> lgr) : Controller
    {
        private readonly ILogger<EmailContactController> _logger = lgr;
        private readonly EwkIqxObdContext _context = ctx;
        public async Task<IActionResult> Index()
        {
            List<EqoContactInfo> c = await _context.EqoContactInfo.ToListAsync();
            return View(c);
        }
    }
}
