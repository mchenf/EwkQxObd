using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/contract")]
    public class ApiContractController : Controller
    {
        private readonly ILogger<ApiContractController> _logger;
        private readonly EwkIqxObdContext _context;

        public ApiContractController(ILogger<ApiContractController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{contractNumber}")]
        public async Task<EqoContract?> Get([FromRoute]long contractNumber)
        {
            var Result = await _context.EqoContract
                .Where(c => c.ContractNumber == contractNumber)
                .Include(c => c.CustomerContact)
                .Include(c => c.EmployeeResponsible)
            .FirstOrDefaultAsync();

            return Result;
        }

        [HttpGet]
        public IEnumerable<EqoContract> Details()
        {
            return _context.EqoContract;

        }


        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> NewContractSingle(EqoContract contract)
        {

            await _context.EqoContract.AddAsync(contract);

            await _context.SaveChangesAsync();

            return Ok(new { Consumes = "application/json", Values = contract });




        }

    }
}
