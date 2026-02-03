using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/account")]
    public class ApiAccountController : Controller
    {
        private readonly ILogger<ApiAccountController> _logger;
        private readonly EwkIqxObdContext _context;

        public ApiAccountController(ILogger<ApiAccountController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{partnerId}")]
        public async Task<IqxOrganization?> Get([FromRoute] int partnerId)
        {
            var Result = await _context.IqxOrganisation
                .Where(a => a.AccountNumber == partnerId).FirstOrDefaultAsync();

            return Result;
        }


        [HttpGet]
        public IEnumerable<IqxOrganization> Details()
        {
            return _context.IqxOrganisation;

        }


        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> NewAccountSingle(IqxOrganization Account)
        {

            await _context.IqxOrganisation.AddAsync(Account);

            await _context.SaveChangesAsync();

            return Ok(new { Consumes = "application/json", Values = Account });

        }

    }
}
