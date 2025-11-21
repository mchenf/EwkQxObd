using EwkQxObd.Core.Model.Views;
using EwkQxObd.WebApi.Data;
using EwkQxObd.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Enumeration;

namespace EwkQxObd.WebApi.Controllers
{
    [Route("[Controller]")]
    public class SyngioController : Controller
    {
        private EwkIqxObdContext _context;

        public SyngioController(EwkIqxObdContext ctx)
        {
            _context = ctx;
        }
        public async Task<IActionResult> Index()
        {
            var systems = await _context.SyngioViewSystems.ToListAsync();

            return View(systems);
        }



        [Route("[Action]/{SystemToFind}")]
        public async Task<IActionResult> ListNetworks([FromRoute] string SystemToFind)
        {
            var networks = await _context.SyngioViewNetworks.Where((n) =>
                    n.System == SystemToFind
                ).ToListAsync();

            return View(networks);

        }



        [Route("[Action]/{SerialNumber}")]
        public async Task<IActionResult> GetByInstrumentSN([FromRoute] string SerialNumber)
        {
            var network = await (
                from a in _context.Syngio
                where a.SerialNumber == SerialNumber
                select a
            ).FirstOrDefaultAsync();

            if (network is null) {

                return NoContent();

            }


            var result = await (
                from a in _context.Syngio
                where a.NetworkId == network.NetworkId && a.System == network.System
                select a
            ).ToListAsync();

            return Ok(result);
        }

        [Route("[Action]/{id}")]
        public async Task<IActionResult> Details([FromRoute] long id)
        {
            var inst = await (
                from i in _context.Syngio
                where i.Id == id
                select i
            ).FirstOrDefaultAsync();

            if (inst is null)
            {

                return NoContent();

            }

            return View(inst);
        }


        [Route("[Action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchPost(Syngio search)
        {
            var inst = await (
                from i in _context.Syngio
                where i.SerialNumber == search.SerialNumber
                select i
            ).FirstOrDefaultAsync();


            return View(nameof(Search), inst);
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult Search(Syngio search)
        {
            return View(search);
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult Upload()
        {
            return View(new NetworkCsvUploadViewModel { FromWebCall = false, SelectedSystem = "Pacific" });
        }

    }
}
