using EwkQxObd.Core.Model.Views;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers
{
    [Route("[Controller]")]
    public class SysnetinstController : Controller
    {
        public SysnetinstController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("[Action]/{SerialNumber}")]
        public async Task<IActionResult> GetByInstrumentSN([FromRoute] string SerialNumber, [FromServices] EwkIqxObdContext _context)
        {
            var network = await (
                from a in _context.VwSysnetinstorg
                where a.SerialNumber == SerialNumber
                select a
            ).FirstOrDefaultAsync();

            if (network is null) {

                return NoContent();

            }


            var result = await (
                from a in _context.VwSysnetinstorg
                where a.NetworkId == network.NetworkId && a.System == network.System
                select a
            ).ToListAsync();

            return Ok(result);
        }
    }
}
