using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/ContractObject")]
    public class ApiContractObjectController : Controller
    {
        private readonly ILogger<ApiContractObjectController> _logger;
        private readonly EwkIqxObdContext _context;

        public ApiContractObjectController(ILogger<ApiContractObjectController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var result = await _context.EqoContractObject
                .Include(c => c.Contract)
                .Include(c => c.ShipTo)
                .ToListAsync();
            if (result != default)
            {
                return Ok( new {ContentType = "application/json", Values = result });
            }
            return NoContent();
        }


        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> NewContractSingle(EqoContractObject contractObj)
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

            return Ok(new { Consumes = "application/json", Values = contractObjToSync });


        }

    }
}
