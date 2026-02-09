using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class ContractController(
        EwkIqxObdContext ctx
        ) : Controller
    {

        private readonly EwkIqxObdContext _context = ctx;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] EqoContract Contract)
        {
            if (!ModelState.IsValid)
            {
                return View(Contract);
            }

            throw new NotImplementedException();

        }

        [HttpGet]
        [Route("{ContractNo}")]
        public async Task<IActionResult> Detail([FromRoute] int ContractNo)
        {
            var contracts = await _context.InstrumentLinkStatus
                .Where(I => I.ContractNumber == ContractNo)
                .Include(I => I.ShippedTo)
                .Include(I => I.ConnectedTo)
                .ToListAsync();

            if (contracts is null)
            {
                return NoContent();
            }
            else
            {
                return View(contracts);
            }

        }



    }
}
