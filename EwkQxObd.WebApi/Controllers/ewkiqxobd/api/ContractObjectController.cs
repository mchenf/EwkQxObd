using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/[controller]")]
    public class ContractObjectController : Controller
    {
        private readonly ILogger<ContractObjectController> _logger;
        private readonly EwkIqxObdContext _context;

        public ContractObjectController(ILogger<ContractObjectController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<EqoContract> Details()
        {
            return _context.EqoContract;

        }


        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> NewContractSingle(EqoContractObject contractObj)
        {
            var contractObjToSync = new EqoContractObject
            {
                SerialNumber = contractObj.SerialNumber,
                InstrumentType = contractObj.InstrumentType
            };

            if (contractObj.ShipTo != default)
            {
                var shipTo = await _context.EqoAccount.FirstOrDefaultAsync(acc => acc.PartnerId == contractObj.ShipTo.PartnerId);
                contractObjToSync.ShipTo = shipTo;
                contractObjToSync.EqoAccountId = shipTo!.Id;
            }
            if (contractObj.Contract != default)
            {
                var contract = await _context.EqoContract.FirstOrDefaultAsync(con => con.ContractNumber == contractObj.Contract.ContractNumber);
                contractObjToSync.Contract = contract;
                contractObjToSync.EqoContractId = contract!.Id;
            }

            await _context.EqoContractObject.AddAsync(contractObjToSync);

            await _context.SaveChangesAsync();

            return Ok(new { Consumes = "application/json", Values = contractObjToSync });




        }

    }
}
