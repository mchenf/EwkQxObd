using EwkQxObd.Core.Model;
using EwkQxObd.Core.Model.Views;
using EwkQxObd.WebApi.Data;
using EwkQxObd.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class ContractObjectController : Controller
    {

        private readonly ILogger<ContractObjectController> _logger;
        private EwkIqxObdContext _context;

        public ContractObjectController(EwkIqxObdContext ctx, ILogger<ContractObjectController> logger)
        {
            _context = ctx;
            _logger = logger;
        }

        private readonly int itemsPerPage = 16;

        [HttpGet()]
        [Route("{Page:int?}")]
        public async Task<IActionResult> Index([FromRoute]int Page = 1)
        {

            var initQuery = _context.Vinlks
                .Where(v => !v.IsMissingContract)
                .OrderByDescending(v => v.RecordedAt);



            int totalRows = await initQuery.CountAsync();

            int ItemsToSkip = (Page - 1) * itemsPerPage;

            var vinlks = await initQuery
                .Skip(ItemsToSkip)
                .Take(16).ToListAsync();


            ViewBag.TotalRows = totalRows;
            ViewBag.TotalPages = (totalRows + itemsPerPage) / itemsPerPage;
            ViewBag.CurrPage = Page;

            return View(vinlks);
        }

        [HttpGet()]
        public IActionResult Search([FromRoute]ContractObjSearchPageModel model)
        {
            return View(model);
        }
        

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchPost(ContractObjSearchPageModel model)
        {

            if (model.FilterApplied == null || 
                model.FilterApplied.LoadState == 
                ContractObjSearchTermLoadState.None )
            {
                return RedirectToAction(nameof(Search));
            }

            IQueryable<Vinlks> iqy = _context.Vinlks;

            var filter = model.FilterApplied;


            if (ContractObjSearchTermLoadState.ContractNumber == (filter.LoadState & ContractObjSearchTermLoadState.ContractNumber))
            {
                iqy = iqy.Where(co => co.ContractNumber == filter.ContractNumber);
            }

            if (ContractObjSearchTermLoadState.PartnerAccountNumber == (filter.LoadState & ContractObjSearchTermLoadState.PartnerAccountNumber))
            {
                iqy = iqy.Where(co => co.PartnerId == filter.PartnerAccountNumber);
            }

            if (ContractObjSearchTermLoadState.SerialNumber == (filter.LoadState & ContractObjSearchTermLoadState.SerialNumber))
            {
                iqy = iqy.Where(co => co.SerialNumber == filter.SerialNumber);
            }

            if (ContractObjSearchTermLoadState.System == (filter.LoadState & ContractObjSearchTermLoadState.System) &&
                ContractObjSearchTermLoadState.NetworkID == (filter.LoadState & ContractObjSearchTermLoadState.NetworkID))
            {
                iqy = iqy.Where(co => co.System == filter.System && co.NetworkId == filter.NetworkID);
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


        private const string debugMessage = "[{0}] {1}";
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(EqoContractObject contractObj)
        {
            _logger.BeginScope(nameof(New));
            _logger.LogDebug(debugMessage, "Step 010", "Starting Sync of New Contract Object");

            var DataSynker = new ContractObjectDataSynker(_context, contractObj);

            var dataSyncResult = await DataSynker.SyncSingleCobj();


            if (dataSyncResult.MissingState > ContractObjectDataMissing.AllGreen)
            {
                return BadRequest("Contract object must have a linking contract and a ship-to account.");
            }

            await _context.EqoContractObject.AddAsync(contractObj);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new {Id = contractObj.Id});
        }

        [HttpGet]
        public async Task<IActionResult> Detail([FromQuery]int Id)
        {
            var objFound = await _context.EqoContractObject
                    .Include(co => co.Contract)
                    .Include(co => co.ShipTo)
                    .Include(co => co.Contract!.CustomerContact)
                    .Include(co => co.Contract!.EmployeeResponsible)
                    .FirstOrDefaultAsync(o => o.Id == Id);
            if (objFound == default)
            {
                return NoContent();
            }

            objFound.InstrumentConnected = await _context.Syngio.FirstOrDefaultAsync(
                o => o.SerialNumber == objFound.SerialNumber
            );

            return View(objFound);
        }

        [HttpGet]
        [Route("{ContractNumber:int}")]
        public async Task<IActionResult> ByContract([FromRoute]int ContractNumber)
        {
            var contractFound = await _context.Syngio.Where(
                o => o.ContractNumber != null && o.ContractNumber == ContractNumber
            ).ToListAsync();

            if (contractFound == default)
            {
                return NoContent();
            }
            
            return View(contractFound);
        }
    }
}
