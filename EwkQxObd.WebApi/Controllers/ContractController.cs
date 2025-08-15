using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EwkQxObd.WebApi.Controllers
{

    [Route("[controller]")]
    public class ContractController : Controller
    {
        private EwkIqxObdContext _context;
        private ILogger<ContractController> _logger;


        public ContractController(EwkIqxObdContext ctx, ILogger<ContractController> logger)
        {
            _context = ctx;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            _logger.BeginScope(nameof(Index));
            _logger.LogTrace(
                new EventId(3322),
                "Starting procedure for getting all contracts."
            );
            var contracts = await _context.EqoContract.ToListAsync();
            if (contracts is null)
            {
                return NoContent();
            }
            return View(contracts);
        }

        [Route("{contractID}")]
        public async Task<IActionResult> SingleRead([FromRoute] long contractID)
        {
            _logger.BeginScope(nameof(SingleRead));
            _logger.LogTrace(
                new EventId(3321),
                "Starting procedure for getting just one contract."
            );


            var contractFound = 
                await _context.EqoContract
                .FirstOrDefaultAsync(
                    c => c.ContractNumber == contractID
                );

            if (contractFound is null)
            {
                return NoContent();
            }

            return View(contractFound);


        }
    }
}
