using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using EwkQxObd.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class ContractObjectController : Controller
    {
        private EwkIqxObdContext _context;


        public ContractObjectController(EwkIqxObdContext ctx)
        {
            _context = ctx;
        }


        public async Task<IActionResult> Index()
        {
            var objFound = await _context.EqoContractObject
                    .Include(co => co.Contract)
                    .Include(co => co.ShipTo)
                    .Include(co => co.Contract!.CustomerContact)
                    .Include(co => co.Contract!.EmployeeResponsible)
                    .ToListAsync();
            return View(objFound);
        }

        [HttpGet()]
        public IActionResult Search([FromRoute]ContractObjSearchPageModel model)
        {
            return View(model);
        }
        

        [HttpPost()]
        public async Task<IActionResult> SearchPost(ContractObjSearchPageModel model)
        {

            if (model.FilterApplied == null || 
                model.FilterApplied.LoadState == 
                ContractObjSearchTermLoadState.None )
            {
                return RedirectToAction(nameof(Search));
            }

            IQueryable<EqoContractObject> iqy = _context.EqoContractObject
                    .Include(co => co.Contract)
                    .Include(co => co.ShipTo);

            var filter = model.FilterApplied;


            if (ContractObjSearchTermLoadState.ContractNumber == (filter.LoadState & ContractObjSearchTermLoadState.ContractNumber))
            {
                iqy = iqy.Where(co => co.Contract!.ContractNumber == filter.ContractNumber);
            }

            if (ContractObjSearchTermLoadState.PartnerAccountNumber == (filter.LoadState & ContractObjSearchTermLoadState.PartnerAccountNumber))
            {
                iqy = iqy.Where(co => co.ShipTo!.PartnerId == filter.PartnerAccountNumber);
            }

            if (ContractObjSearchTermLoadState.SerialNumber == (filter.LoadState & ContractObjSearchTermLoadState.SerialNumber))
            {
                iqy = iqy.Where(co => co.SerialNumber == filter.SerialNumber);
            }

            var result = await iqy
                .ToListAsync();


            model.Results = result;

            return View(nameof(Search), model);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(EqoContractObject contractObj)
        {

            var dataSync = new ContractObjectDataSync();
            var dataSyncResult = await dataSync.SyncSingle(_context, contractObj);
            if (dataSyncResult.MissingState > ContractObjectDataMissing.AllGreen)
            {
                return BadRequest("Contract object must have a linking contract and a ship-to account.");
            }

            await _context.EqoContractObject.AddAsync(contractObj);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(New));
        }

        [HttpGet("")]
        public async Task<IActionResult> ViewContractObject([FromQuery]long Id)
        {
            var objFound = await _context.EqoContractObject
                    .Include(co => co.Contract)
                    .Include(co => co.ShipTo)
                    .Include(co => co.Contract!.CustomerContact)
                    .Include(co => co.Contract!.EmployeeResponsible)
                    .FirstOrDefaultAsync(o => o.id == Id);


            return View("New", objFound);
        }
    }
}
