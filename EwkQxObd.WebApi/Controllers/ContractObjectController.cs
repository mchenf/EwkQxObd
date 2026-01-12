using EwkQxObd.Core.Model;
using EwkQxObd.Core.Model.Views;
using EwkQxObd.WebApi.Data;
using EwkQxObd.WebApi.Models;
using EwkQxObd.WebApi.ModelsPage.ContractObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class ContractObjectController : Controller
    {

        private readonly ILogger<ContractObjectController> _logger;
        private readonly EwkIqxObdContext _context;
        private readonly int itemsPerPage = 16;

        private readonly CojtIndexFilter Filter = new();

        private IQueryable<Vinlks> VinlkQuery { get; set; }

        public ContractObjectController(
            EwkIqxObdContext ctx, 
            ILogger<ContractObjectController> logger)
        {
            _logger = logger;
            _context = ctx;

            VinlkQuery = _context.Vinlks
                .Where(v => !v.IsMissingContract)
                .OrderByDescending(v => v.RecordedAt);
        }

        [HttpGet()]
        [Route("{Page:int?}")]
        public async Task<IActionResult> Index([FromRoute]int Page = 1)
        {

            int totalRows = await VinlkQuery.CountAsync();
            int ItemsToSkip = (Page - 1) * itemsPerPage;
            var vinlks = await VinlkQuery
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

        [HttpGet]
        public async Task<IActionResult> SearchShipTo([FromQuery] string TextToSearch)
        {
            var vinklks = await _context.Vinlks
                .Where(v => v.ShipToName!.Contains(TextToSearch) || v.AccountName!.Contains(TextToSearch))
                .ToListAsync();



            ViewBag.TotalRows = vinklks.Count;
            ViewBag.TotalPages = (vinklks.Count + itemsPerPage) / itemsPerPage;
            ViewBag.CurrPage = 1;
            return View(nameof(Index), vinklks);
        }

        [HttpGet]
        public async Task<IActionResult> FilterIndex([FromQuery] bool HideOnboarded = false)
        {
            if (!HideOnboarded)
            {
                return await Index();
            }
            var vinklks = await _context.Vinlks
                .Where(v => v.IsMissingContract | v.IsMissingDblink | v.IsMissingOrgSync | v.IsMissingSystem | v.IsMissingNetwork == false)
                .ToListAsync();
            ViewBag.TotalRows = vinklks.Count;
            ViewBag.TotalPages = (vinklks.Count + itemsPerPage) / itemsPerPage;
            ViewBag.CurrPage = 1;
            return View(nameof(Index), vinklks);
        }

        [HttpPost]
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
                iqy = iqy.Where(co => co.AccountNumber == filter.PartnerAccountNumber);
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
        public async Task<IActionResult> AddOn([FromQuery]int ContractId, [FromQuery]int ShipToId = 0, [FromQuery]bool AsTemplate = false)
        {
            EqoContract? contract = null;
            EqoAccount? shipTo = null;
            if (ShipToId > 0)
            {
                shipTo = 
                    await _context.EqoAccount
                    .FirstOrDefaultAsync(c => c.Id == ShipToId);
            }

            contract =
                await _context.EqoContract
                .Include(c => c.EmployeeResponsible)
                .Include(c => c.CustomerContact)
                .FirstOrDefaultAsync(c => c.Id == ContractId);

            if (contract == null)
            {
                return BadRequest($"Contract ID Not Found for {ContractId}");
            }

            EqoContractObject obj = new()
            {
                Contract = contract,
                ShipTo = shipTo
            };

            if (AsTemplate)
            {
                obj.Contract.Id = null;
                obj.Contract.ContractNumber = null;
                obj.Contract.Description = string.Empty;
            }

            return View(nameof(New), obj);
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
        [Route("{ContractObjectId}")]
        public async Task<IActionResult> Detail([FromRoute] int ContractObjectId)
        {
            var objFound = await _context.EqoContractObject
                    .Include(co => co.Contract)
                    .Include(co => co.ShipTo)
                    .Include(co => co.Contract!.CustomerContact)
                    .Include(co => co.Contract!.EmployeeResponsible)
                    .FirstOrDefaultAsync(o => o.Id == ContractObjectId);
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
