using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Controllers.ewkiqxobd.Common;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/contract")]
    public class ApiContractController : Controller
    {
        private readonly ILogger<ApiContractController> _logger;
        private readonly EwkIqxObdContext _context;
        private readonly Helper _helper;

        public ApiContractController(
            ILogger<ApiContractController> logger, 
            EwkIqxObdContext dataContext, 
            Helper helper)
        {
            _logger = logger;
            _context = dataContext;
            _helper = helper;
        }

        [HttpGet("{contractNumber}/[Action]")]
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
        [Produces("application/json")]
        public async Task<IActionResult> Index([FromQuery] int Offset = 0, [FromQuery] int Take = 50)
        {
            return await _helper.PaginatedListAll<EqoContract>(
                Offset,
                Take,
                () => _context.EqoContract.AsQueryable(),
                () => BadRequest("Offset must be greater than 0, Take must be greater than 1."),
                c => Ok(c)
             );
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

        [HttpGet("[Action]")]
        [Produces("application/json")]
        public async Task<IActionResult> Match([FromQuery] int ContractNo)
        {

            int digits = GetDigit(ContractNo);

            if (digits < 3 || digits > 8)
            {
                return BadRequest();
            }

            int Lbound = ContractNo;
            int UBound = ContractNo + 1;

            var Query = _context.EqoContract.AsQueryable();

            int NBound = 8 - digits;

            var Bounds = new List<Expression<Func<EqoContract, bool>>>();

            for (int i = 0; i < NBound; i++)
            {
                var l = Lbound;
                var u = UBound;

                Bounds.Add(c => c.ContractNumber >= l && c.ContractNumber < u);

                Lbound *= 10;
                UBound *= 10;
            }

            Expression<Func<EqoContract, bool>> combined = Bounds[0];

            foreach (var p in Bounds.Skip(1))
            {
                combined = combined.OrElse(p);
            }

            if (combined is not null)
            {
                Query = Query.Where(combined);
            }


            var Result = await Query.ToListAsync();

            if (Result is null)
            {
                return NoContent();
            }

            return Ok(Result);
        }

        private int GetDigit(int n)
        {
            if (n == 0)
            {
                return 1;
            }

            if (n < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            int count = 0;

            while (n > 0)
            {
                n /= 10;
                count++;
            }
            return count;
        }

    }
}
