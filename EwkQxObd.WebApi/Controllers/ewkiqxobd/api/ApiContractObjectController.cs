using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/ContractObject")]
    public class ApiContractObjectController : Controller
    {
        private readonly ILogger<ApiContractObjectController> _logger;
        private readonly EwkIqxObdContext _context;

        public ApiContractObjectController(ILogger<ApiContractObjectController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EqoContractObject NewContractObj)
        {
            if (NewContractObj == null)
            {
                return BadRequest(new
                {
                    Message = "Must supply a valid, non-null contract object."
                });
            }

            bool IsDuplicate = await _context.EqoContractObject.AnyAsync(c => c.Id == NewContractObj.Id);

            if (IsDuplicate)
            {
                return BadRequest(new
                {
                    Message = $"Duplicated ID: {NewContractObj.Id}."
                });
            }

            await _context.EqoContractObject.AddAsync(NewContractObj);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "ContractObject Created.",
                New = NewContractObj
            });
        }
        [HttpGet("{ContractObjectId}")]
        public async Task<IActionResult> Read([FromRoute] int ContractObjectId)
        {
            var Found = await _context.EqoContractObject.FindAsync(ContractObjectId);

            if (Found is null)
            {
                return NotFound(new
                {
                    Message = $"Object with ID {ContractObjectId} does not exist."
                });
            }

            return Ok(Found);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Index([FromQuery]int Offset = 0, [FromQuery]int Take = 50)
        {
            if (Offset < 0 || Take <= 0)
            {
                return BadRequest("Offset must be greater than 0, Take must be greater than 1.");
            }

            var query = _context.EqoContractObject.AsQueryable();

            int total = await query.CountAsync();


            var items = await query
                .Skip(Offset)
                .Take(Take)
                .ToListAsync();

            var result = new
            {
                Offset,
                Take,
                Total = total,
                Items = items.Count,
                Data = items
            };



            return Ok(result);
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> Details()
        {
            var result = await _context.EqoContractObject
                .Include(c => c.Contract)
                .Include(c => c.ShipTo)
                .ToListAsync();
            if (result != default)
            {
                return Ok( new {contentType = "application/json", values = result });
            }
            return NoContent();
        }

        [HttpGet("[Action]")]
        [Produces("application/json")]
        public async Task<IActionResult> BusinessUnits()
        {
            var BU = await _context.InstrumentTypes
                .Select(c => c.BU)
                .Distinct()
                .ToListAsync();
            return Ok(new { contentType = "application/json", values = BU });
        }

        [HttpGet("[Action]")]
        [Produces("application/json")]
        public async Task<IActionResult> TopInstTypes()
        {
            var INST = await _context.VwTopInstrumentTypes
                .FromSqlRaw("" +
                "SELECT T.InstrumentTypeID, T.Name, T.BU, T.ShortName, COUNT(C.Id) AS Usage\r\nFROM         Eqo.ContractObject AS C LEFT OUTER JOIN\r\n                         iqx.InstrumentType AS T ON C.InstrumentTypeId = T.InstrumentTypeID\r\nWHERE     (T.InstrumentTypeID IS NOT NULL)\r\nGROUP BY T.InstrumentTypeID, T.Name, T.BU, T.ShortName\r\n")
                .ToListAsync();
            var OrderedInst = INST.OrderByDescending(T => T.Usage);
            return Ok(new { contentType = "application/json", values = OrderedInst });
        }

        [HttpGet("[Action]/{BusinessUnit}")]
        [Produces("application/json")]
        public async Task<IActionResult> InstrumentType([FromRoute]string BusinessUnit)
        {
            if (string.IsNullOrEmpty(BusinessUnit))
            {
                return BadRequest();
            }
            var InstType = await _context.InstrumentTypes
                .Where(t => t.BU == BusinessUnit)
                .ToListAsync();
            if (!InstType.Any())
                return NoContent();
            else
                return Ok(new { contentType = "application/json", values = InstType });
        }


        [HttpPost("[Action]")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> NewContractSingle(EqoContractObject contractObj)
        {
            var contractObjToSync = new EqoContractObject
            {
                SerialNumber = contractObj.SerialNumber,
                InstrumentType = contractObj.InstrumentType
            };

            bool refuseSync = false;

            if (contractObj.ShipTo != default)
            {
                var shipTo = await _context.Organization.FirstOrDefaultAsync(acc => acc.AccountNumber == contractObj.ShipTo.AccountNumber);
                if (shipTo == default)
                {
                    contractObjToSync.ShipTo = contractObj.ShipTo;
                }
                else
                {
                    contractObjToSync.ShipTo = shipTo;
                    contractObjToSync.ShipToId = shipTo.AccountNumber;
                }
            }
            else
            {
                refuseSync = true;
            }


            if (contractObj.Contract != default)
            {
                var contract = await _context.EqoContract.FirstOrDefaultAsync(con => con.ContractNumber == contractObj.Contract.ContractNumber);

                if (contract == default)
                {
                    contractObjToSync.Contract = contractObj.Contract;
                }
                else
                {
                    contractObjToSync.Contract = contract;
                    contractObjToSync.ContractId = contract.Id;
                }
            }
            else
            {
                refuseSync = true;
            }

            if (refuseSync)
            {
                return BadRequest("Contract object must have a linking contract and a ship-to account.");
            }

            await _context.EqoContractObject.AddAsync(contractObjToSync);

            await _context.SaveChangesAsync();

            return Ok(new { Consumes = "application/json", Values = contractObjToSync });


        }

    }
}
