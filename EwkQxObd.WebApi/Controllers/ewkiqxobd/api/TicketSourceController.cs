using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/[controller]")]
    public class TicketSourceController : Controller
    {
        private readonly ILogger<TicketSourceController> _logger;
        private readonly EwkIqxObdContext _context;

        public TicketSourceController(ILogger<TicketSourceController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDetails()
        {
            var result = await _context.EqoTicketSource
                .Include(c => c.IqxContracts)
                .ToListAsync();
            if (result != default)
            {
                return Ok( new {ContentType = "application/json", Values = result });
            }
            return NoContent();
        }


        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> NewTicketSingle(EqoTicketSource ticketSource)
        {
            
            await _context.EqoTicketSource.AddAsync(ticketSource);

            await _context.SaveChangesAsync();

            return Ok(new { Consumes = "application/json", Values = ticketSource });




        }

    }
}
