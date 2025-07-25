
using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Conversion;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/[controller]")]
    public class OrganisationController : Controller
    {
        private readonly ILogger<OrganisationController> _logger;
        private readonly EwkIqxObdContext _context;

        public OrganisationController(ILogger<OrganisationController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<IqxOrganization> Get()
        {
            return _context.IqxOrganisation;
        }

        [HttpPost("bulk")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateOrganizations(IEnumerable<IqxOrganization> orgs)
        {
            

            await _context.IqxOrganisation.AddRangeAsync(orgs);

            await _context.SaveChangesAsync();

            return Ok(new { Consumes = "application/json", Values = orgs });
        }

        [HttpPost()]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateOrganization(IqxOrganization org)
        {


            await _context.IqxOrganisation.AddRangeAsync(org);

            await _context.SaveChangesAsync();

            return Ok(new { Consumes = "application/json", Values = org });
        }

    }
}
