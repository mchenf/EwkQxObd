using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/{controller}")]
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly EwkIqxObdContext _context;

        public ContactController(ILogger<ContactController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Index()
        {
            var results = await _context.EqoContactInfo.ToListAsync();
            return Ok(results);
        }

        [HttpGet("byemail/{emailAddress}")]
        public async Task<EqoContactInfo?> GetByEmail([FromRoute] string emailAddress)
        {
            string decoded = WebUtility.UrlDecode(emailAddress);
            var Result = await _context.EqoContactInfo
                .Where(a => a.EmailAddress == emailAddress).FirstOrDefaultAsync();

            return Result;
        }

        [HttpOptions("{TextToSearch}")]
        public async Task<IActionResult> GetOptionByName([FromRoute] string TextToSearch)
        {
            throw new NotImplementedException();
        }

    }
}
