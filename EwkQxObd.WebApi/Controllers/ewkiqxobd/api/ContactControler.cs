using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/[controller]")]
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly EwkIqxObdContext _context;

        public ContactController(ILogger<ContactController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpGet("[action]")]
        [Produces("application/json")]
        public async Task<IActionResult> List()
        {
            var results = await _context.EqoContactInfo.ToListAsync();
            return Ok(results);
        }

        [HttpGet("[action]")]
        [Produces("application/json")]
        public async Task<IActionResult> Match([FromQuery]string Text)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return BadRequest(new
                {
                    Message = "Search text must not be null"
                });
            }

            var results = await _context.EqoContactInfo
                .Where(
                    c => (!string.IsNullOrEmpty(c.EmailAddress) && c.EmailAddress.StartsWith(Text)) || c.FullName.StartsWith(Text)
                ).ToListAsync();
            if (results.Count == 0)
            {
                return NoContent();

            } 
            return Ok(results);

        }

        [HttpGet("{ContactId}")]
        [Produces("application/json")]
        public async Task<IActionResult> Read([FromRoute]int ContactId)
        {
            var result = await _context.EqoContactInfo.FindAsync(ContactId);

            if (result == null)
            {
                return NoContent();

            }

            return Ok(result);

        }
    }
}
