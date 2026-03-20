using EwkQxObd.Core.Model.Iqx;

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
        public IEnumerable<Organization> Get()
        {
            return _context.Organization;
        }

        [HttpPost("bulk")]
        [Consumes("application/json")]
        public async Task<IActionResult> AddBulk(IEnumerable<Organization> orgs)
        {
            List<Organization> dups = [];
            List<Organization> added = [];

            foreach (var org in orgs)
            {
                if (org == default)
                {
                    continue;
                }
                var found = await _context.Organization
                    .FirstOrDefaultAsync(a => a.AccountNumber == org.AccountNumber);

                if (found == default)
                {
                    added.Add(org);
                    await _context.Organization.AddAsync(org);
                }
                else
                {
                    found.GeisGuid = org.GeisGuid;
                    found.City = org.City;
                    found.Street = org.Street;

                    dups.Add(org);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { ContentType = "application/json", Duplicates = dups, Added = added });
        }

        [HttpPost()]
        [Consumes("application/json")]
        public async Task<IActionResult> AddSingle(Organization org)
        {


            await _context.Organization.AddAsync(org);

            await _context.SaveChangesAsync();

            return Ok(new { Consumes = "application/json", Values = org });
        }

    }
}
