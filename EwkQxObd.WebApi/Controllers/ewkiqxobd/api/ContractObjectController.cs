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
            var account = await _context.EqoAccount.Where(
                acc => acc.PartnerId == contractObj.AccountNumber)
                .FirstOrDefaultAsync();

            var contract = await _context.EqoContract.Where(
                con => con.ContractNumber == contractObj.ContractNumber)
                .FirstOrDefaultAsync();

            if (account != default(EqoAccount))
            {
                contractObj.ShipTo = new EqoAccount { Id = account.Id };
            }

            if (contract != default(EqoContract))
            {
                contractObj.Contract = new EqoContract {Id = contract.Id};
            }


            await _context.EqoContractObject.AddAsync(contractObj);

            await _context.SaveChangesAsync();

            return Ok(new { Consumes = "application/json", Values = contractObj });




        }

    }
}
