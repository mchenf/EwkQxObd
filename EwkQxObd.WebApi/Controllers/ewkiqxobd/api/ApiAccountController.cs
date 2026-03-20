using EwkQxObd.Core.Model.Iqx;
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
        public async Task<Organization?> Get([FromRoute] int partnerId)
        {
            var Result = await _context.Organization
                .Where(a => a.AccountNumber == partnerId).FirstOrDefaultAsync();

            return Result;
        }


        [HttpGet]
        public IEnumerable<Organization> Details()
        {
            return _context.Organization;

        }


        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Create(Organization Account)
        {

            await _context.Organization.AddAsync(Account);

            await _context.SaveChangesAsync();

            return Ok(new { ContentType = "application/json", Data = Account });

        }

    }
}
