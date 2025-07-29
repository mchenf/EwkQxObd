using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly EwkIqxObdContext _context;

        public AccountController(ILogger<AccountController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        public IActionResult Index()
        {
            return View();
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
