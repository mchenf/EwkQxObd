
using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Conversion;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> AddBulk(IEnumerable<IqxOrganization> orgs)
        {
            List<IqxOrganization> dups = new();
            List<IqxOrganization> added = new();

            foreach (var org in orgs)
            {
                if (org == default)
                {
                    continue;
                }
                var found = await _context.IqxOrganisation
                    .FirstOrDefaultAsync(a => a.GeisGuid == org.GeisGuid);

                if (found == default)
                {
                    added.Add(org);
                    await _context.IqxOrganisation.AddAsync(org);
                }
                else
                {
                    dups.Add(org);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { ContentType = "application/json", Duplicates = dups, Added = added });
        }

        [HttpPost()]
        [Consumes("application/json")]
        public async Task<IActionResult> AddSingle(IqxOrganization org)
        {


            await _context.IqxOrganisation.AddAsync(org);

            await _context.SaveChangesAsync();

            return Ok(new { Consumes = "application/json", Values = org });
        }

    }
}
