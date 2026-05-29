using EwkQxObd.Core.Model;
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
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Create([FromBody] Organization NewOrg)
        {
            if (NewOrg == null)
            {
                return BadRequest(new
                {
                    Message = "Must supply a valid, non-null organization."
                });
            }

            bool IsDuplicate = await _context.Organization.AnyAsync(c => c.AccountNumber == NewOrg.AccountNumber);

            if (IsDuplicate)
            {
                return BadRequest(new
                {
                    Message = $"Duplicated ID: {NewOrg.AccountNumber}."
                });
            }

            await _context.Organization.AddAsync(NewOrg);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Organization Created.",
                New = NewOrg
            });
        }

        [HttpGet("{AccountNumber}")]
        [Produces("application/json")]
        public async Task<IActionResult> Read([FromRoute] int AccountNumber)
        {
            var Found = await _context.Organization.FindAsync(AccountNumber);

            if (Found is null)
            {
                return NotFound(new
                {
                    Message = $"We did not find organization with account number {AccountNumber}."
                });
            }

            return Ok(Found);
        }

        [HttpPut("{AccountNumber}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Update([FromRoute] int AccountNumber, [FromBody] Organization UpdateOrg)
        {
            if (AccountNumber != UpdateOrg.AccountNumber)
            {
                return BadRequest(new
                {
                    Message = "Compare carefully the id to target and the one in payload object."
                });
            }

            var Found = await _context.Organization.FindAsync(AccountNumber);



            if (Found is null)
            {
                return NotFound(new
                {
                    Message = $"Object with ID {AccountNumber} does not exist."
                });
            }

            Organization old = new()
            {
                AccountNumber = Found.AccountNumber,
                GeisGuid = Found.GeisGuid,
                Name = Found.Name,
                Region = Found.Region,
                Country = Found.Country,
                City = Found.City,
                Street = Found.Street
            };

            Found.GeisGuid = UpdateOrg.GeisGuid;
            Found.Name = UpdateOrg.Name;
            Found.Region = UpdateOrg.Region;
            Found.Country = UpdateOrg.Country;
            Found.City = UpdateOrg.City;
            Found.Street = UpdateOrg.Street;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Update successful.",
                Old = old,
                New = Found
            });
        }

        [HttpDelete("{ContractObjectId}")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete([FromRoute] int AccountNumber)
        {
            var Found = await _context.Organization.FindAsync(AccountNumber);

            if (Found is null)
            {
                return NotFound(new
                {
                    Message = $"Organization with account number {AccountNumber} does not exist."
                });
            }

            _context.Organization.Remove(Found);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = $"Organization {AccountNumber} is deleted.",
                Deleted = Found
            });
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

    }
}
