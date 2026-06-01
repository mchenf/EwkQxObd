using EwkQxObd.Core.Model;
using EwkQxObd.Core.Model.Iqx;
using EwkQxObd.WebApi.Controllers.ewkiqxobd.Common;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/ContractObject")]
    public class ApiContractObjectController : Controller
    {
        private readonly ILogger<ApiContractObjectController> _logger;
        private readonly EwkIqxObdContext _context;

        private readonly Helper _helper;
        public ApiContractObjectController(
            ILogger<ApiContractObjectController> logger, 
            EwkIqxObdContext dataContext,
            Helper helper)
        {
            _logger = logger;
            _context = dataContext;
            _helper = helper;
        }

        private async Task<string> VerifyFK(EqoContractObject NewContractObj)
        {
            StringBuilder sb = new();
            //Verify if instrument type can be found
            bool CanFindInstType = await _context.InstrumentTypes.AnyAsync(c => c.InstrumentTypeID == NewContractObj.InstrumentType);
            if (!CanFindInstType)
            {
                sb.AppendLine("Use a valid instrument type");
            }
            bool CanFindContract = await _context.EqoContract.AnyAsync(c => c.Id == NewContractObj.ContractId);
            if (!CanFindContract)
            {
                sb.AppendLine("Use a valid contract");
            }

            bool CanFindShipTo = await _context.Organization.AnyAsync(c => c.AccountNumber == NewContractObj.ShipToId);
            if (!CanFindShipTo)
            {
                sb.AppendLine("Use a valid organization as shipto");
            }

            return sb.ToString();

        }

        [HttpPost]
        [Produces("application/json")]
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

            var verMessage = await VerifyFK(NewContractObj);
            if (!string.IsNullOrEmpty(verMessage))
            {
                return BadRequest(new
                {
                    Message = verMessage
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
        [Produces("application/json")]
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

        [HttpPut("{ContractObjectId}")]
        [Produces("application/json")]
        public async Task<IActionResult> Update([FromRoute] int ContractObjectId, [FromBody] EqoContractObject Update)
        {
            if (ContractObjectId != Update.Id)
            {
                return BadRequest( new
                {
                    Message = "Compare carefully the id to target and the one in payload object."
                });
            }

            var Found = await _context.EqoContractObject.FindAsync(ContractObjectId);



            if (Found is null)
            {
                return NotFound(new
                {
                    Message = $"Object with ID {ContractObjectId} does not exist."
                });
            }

            var verMessage = await VerifyFK(Update);
            if (!string.IsNullOrEmpty(verMessage))
            {
                return BadRequest(new
                {
                    Message = verMessage
                });
            }

            EqoContractObject old = new ()
            {
                Id = Found.Id,
                InstrumentType = Found.InstrumentType,
                SerialNumber = Found.SerialNumber,
                ContractId = Found.ContractId,
                ShipToId = Found.ShipToId,
            };

            Found.SerialNumber = Update.SerialNumber;
            Found.InstrumentType = Update.InstrumentType;
            Found.ContractId = Update.ContractId;
            Found.ShipToId = Update.ShipToId;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Update successful.",
                Old = old,
                New = Found
            });
        }

        [HttpDelete("{ContractObjectId}")]
        public async Task<IActionResult> Delete([FromRoute] int ContractObjectId)
        {
            var Found = await _context.EqoContractObject.FindAsync(ContractObjectId);

            if (Found is null)
            {
                return NotFound(new
                {
                    Message = $"Object with ID {ContractObjectId} does not exist."
                });
            }

            _context.EqoContractObject.Remove(Found);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = $"ContractObject {ContractObjectId} is deleted.",
                Deleted = Found
            });
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Index([FromQuery]int Offset = 0, [FromQuery]int Take = 50)
        {
            return await _helper.PaginatedListAll<EqoContractObject>(
                Offset,
                Take,
                () => _context.EqoContractObject.AsQueryable(),
                () => BadRequest("Offset must be greater than 0, Take must be greater than 1."),
                c => Ok(c)
             );
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

        [HttpPost("[Action]")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Filter([FromBody] ContractObjectFilter filter)
        {
            var query = _context.EqoContractObject.AsQueryable();

            var conditions = new List<Expression<Func<EqoContractObject, bool>>>();

            if (!string.IsNullOrEmpty(filter.SerialNumber))
            {
                conditions.Add(o => o.SerialNumber == filter.SerialNumber);
            }
            if(filter.InstrumentType is not null)
            {
                conditions.Add(o => o.InstrumentType == filter.InstrumentType);
            }
            if(filter.ContractNumber is not null)
            {
                var contractId = await _context.EqoContract
                    .Where(c => c.ContractNumber == filter.ContractNumber)
                    .Select(c => c.Id)
                    .SingleOrDefaultAsync();
                if(contractId == null)
                {
                    return NotFound(new
                    {
                        Message = $"Contract number {filter.ContractNumber} does not exist."
                    });
                }
                else
                {
                    conditions.Add(o => o.ContractId == contractId);
                }
            }
            if (filter.ShipToNumber is not null)
            {
                conditions.Add(o => o.ShipToId == filter.ShipToNumber);

            }

            Expression<Func<EqoContractObject, bool>>  combined = conditions[0];

            foreach (var q in conditions.Skip(1))
            {
                combined = combined.And(q);

            }

            if (combined is not null)
            {
                query = query.Where(combined);

            }

            var found = await query.ToListAsync();
            if (found == null)
            {
                return NoContent();

            }
            else
            {
                return Ok(found);
            }


        }

        [HttpGet("[Action]")]
        [Produces("application/json")]
        public IActionResult Filter()
        {
            return Ok(new ContractObjectFilter());
        }

    }
}
