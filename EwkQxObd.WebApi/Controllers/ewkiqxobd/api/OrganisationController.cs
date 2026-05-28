using EwkQxObd.Core.Model.Iqx;
using EwkQxObd.WebApi.Controllers.ewkiqxobd.Common;
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

        private readonly Helper _helper;

        public OrganisationController(
            ILogger<OrganisationController> logger, 
            EwkIqxObdContext dataContext,
            Helper helper)
        {
            _logger = logger;
            _context = dataContext;
            _helper = helper;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> ListAll([FromQuery] int Offset = 0, [FromQuery] int Take = 30)
        {

            return await _helper.PaginatedListAll<Organization>(
                Offset,
                Take,
                () => _context.Organization.AsQueryable(),
                () => BadRequest("Offset must be greater than 0, Take must be greater than 1."),
                c => Ok(c)
             );
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
