using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers
{

    [Route("[controller]/[action]")]
    public class TicketSourceController : Controller
    {
        private EwkIqxObdContext _context;


        public TicketSourceController(EwkIqxObdContext ctx)
        {
            _context = ctx;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var results = await _context.EqoTicketSource.ToListAsync();
            return View(results);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EqoTicketSource ticket)
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        public async Task<IActionResult> Details()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch()
        {
            throw new NotImplementedException();

        }
    }
}
