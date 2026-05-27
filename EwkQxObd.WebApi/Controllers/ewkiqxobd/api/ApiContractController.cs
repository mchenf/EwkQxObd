using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/contract")]
    public class ApiContractController : Controller
    {
        private readonly ILogger<ApiContractController> _logger;
        private readonly EwkIqxObdContext _context;

        public ApiContractController(ILogger<ApiContractController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("exist/{contractNumber}")]
        public async Task<bool> Exist([FromRoute] int contractNumber)
        {
            var Result = await _context.EqoContract.AnyAsync(c => c.ContractNumber == contractNumber);

            return Result;
        }

        [HttpGet("{contractNumber}")]
        public async Task<EqoContract?> Get([FromRoute] int contractNumber)
        {
            var Result = await _context.EqoContract
                .Where(c => c.ContractNumber == contractNumber)
                .Include(c => c.CustomerContact)
                .Include(c => c.EmployeeResponsible)
            .FirstOrDefaultAsync();

            return Result;
        }

        [HttpPut("{ContractId}")]
        public async Task<IActionResult> Update([FromRoute] int ContractId, [FromBody] EqoContract contract)
        {
            if (ContractId != contract.Id)
            {
                return BadRequest(new
                {
                    Message = "ContractNumber mismatch between route and body."
                });
            }


            var existing = await _context.EqoContract.
                FirstOrDefaultAsync(c => c.Id == ContractId);
            if (existing is null)
            {
                return NotFound(new
                {
                    Message = $"Contract {contract.Id} cannot be found."
                });
            }

            EqoContract old = new()
            {
                Id = existing.Id,
                ContractNumber = existing.ContractNumber,
                Description = existing.Description,
                ValidFrom = existing.ValidFrom,
                ValidTo = existing.ValidTo,
                CustomerContactId = existing.CustomerContactId,
                EmployeeResponsibleId = existing.EmployeeResponsibleId,
                RecordedAt = existing.RecordedAt,
            };

            existing.ContractNumber = contract.ContractNumber;
            existing.Description = contract.Description;

            existing.ValidFrom = contract.ValidFrom;
            existing.ValidTo = contract.ValidTo;

            existing.CustomerContactId = contract.CustomerContactId;
            existing.EmployeeResponsibleId = contract.EmployeeResponsibleId;

            existing.RecordedAt = contract.RecordedAt;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Update successful.",
                Old = old,
                New = existing
            });

        }

        [HttpGet]
        public IEnumerable<EqoContract> List()
        {
            return _context.EqoContract;

        }


        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> NewContractSingle(EqoContract contract)
        {

            await _context.EqoContract.AddAsync(contract);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                var ie = e.InnerException ?? e; 
                return BadRequest(new
                {
                    Message = "Unable to complete database add operation.",
                    Exception = ie.Message ?? "Not message specified"
                });
            }

            return Ok(new { Message = "New contract created successfully", Values = contract });
        }


        [HttpDelete("{ContractId}")]
        public async Task<IActionResult> Delete([FromRoute] int ContractId)
        {
            var found = await _context.EqoContract.FirstOrDefaultAsync(c => c.Id == ContractId);

            if (found is null)
            {
                return NotFound(new
                {
                    Message = $"No contract with ID: {ContractId} can be found, nothing to delete."
                });
            }

            _context.EqoContract.Remove(found);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = $"Contract {ContractId} is deleted.",
                Deleted = found
            });
        }

    }
}
