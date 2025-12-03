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
        public async Task<EqoAccount?> Get([FromRoute] int partnerId)
        {
            var Result = await _context.EqoAccount
                .Where(a => a.PartnerId == partnerId).FirstOrDefaultAsync();

            return Result;
        }


        [HttpGet]
        public IEnumerable<EqoAccount> Details()
        {
            return _context.EqoAccount;

        }


        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> NewAccountSingle(EqoAccount Account)
        {

            await _context.EqoAccount.AddAsync(Account);

            await _context.SaveChangesAsync();

            return Ok(new { Consumes = "application/json", Values = Account });

        }

    }
}
