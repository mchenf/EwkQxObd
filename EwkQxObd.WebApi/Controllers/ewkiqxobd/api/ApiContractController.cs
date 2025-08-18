using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/[controller]")]
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
