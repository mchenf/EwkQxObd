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
            return View(await _context.VwSysnetinstorg.ToListAsync());
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
            var contractObjToSync = new EqoContractObject
            {
                SerialNumber = contractObj.SerialNumber,
                InstrumentType = contractObj.InstrumentType
            };

            bool refuseSync = false;

            if (contractObj.ShipTo != default)
            {
                var shipTo = await _context.EqoAccount.FirstOrDefaultAsync(acc => acc.PartnerId == contractObj.ShipTo.PartnerId);
                if (shipTo == default)
                {
                    contractObjToSync.ShipTo = contractObj.ShipTo;
                }
                else
                {
                    contractObjToSync.ShipTo = shipTo;
                    contractObjToSync.EqoAccountId = shipTo.Id;
                }
            }
            else
            {
                refuseSync = true;
            }


            if (contractObj.Contract != default)
            {
                var contract = await _context.EqoContract.FirstOrDefaultAsync(con => con.ContractNumber == contractObj.Contract.ContractNumber);

                if (contract == default)
                {
                    contractObjToSync.Contract = contractObj.Contract;
                }
                else
                {
                    contractObjToSync.Contract = contract;
                    contractObjToSync.EqoContractId = contract.Id;
                }

                var empEmail = contractObj.Contract.EmployeeResponsible!.EmailAddress;

                if (!string.IsNullOrEmpty(empEmail))
                {
                    var employeResponsible = await _context.EqoContactInfo.FirstOrDefaultAsync(
                        emp => emp.EmailAddress == empEmail
                    );

                    if (employeResponsible == default)
                    {
                        contractObjToSync.Contract.EmployeeResponsible = contractObj.Contract.EmployeeResponsible;
                    }
                    else
                    {
                        contractObjToSync.Contract.EmployeeResponsible = default;
                        contractObjToSync.Contract.EmployeeResponsibleId = employeResponsible.Id;

                    }
                }
            }
            else
            {
                refuseSync = true;
            }

            if (refuseSync)
            {
                return BadRequest("Contract object must have a linking contract and a ship-to account.");
            }

            await _context.EqoContractObject.AddAsync(contractObjToSync);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
