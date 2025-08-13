using EwkQxObd.Core.Model;
using EwkQxObd.Core.Serialization;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/[controller]")]
    public class NetworkController : Controller
    {
        private readonly ILogger<NetworkController> _logger;
        private readonly EwkIqxObdContext _context;

        public NetworkController(ILogger<NetworkController> logger, EwkIqxObdContext dataContext)
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
            var result = await _context.IqxInstrument
                .Include(c => c.LinkedAccount)
                .ToListAsync();
            if (result != default)
            {
                return Ok( new {ContentType = "application/json", Values = result });
            }
            return NoContent();
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var result = await _context.VwSysnetinst
                .ToListAsync();
            if (result != default)
            {
                return Ok(new { ContentType = "application/json", Values = result });
            }
            return NoContent();
        }



        [HttpPost("csv/upload")]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        public async Task<IActionResult> UploadCsv(IFormFileCollection csvFiles)
        {
            Dictionary<string, int> report = [];
            foreach (var csvFile in csvFiles)
            {

                if (csvFile == default || csvFile.Length == 0)
                {
                    return BadRequest("No file uploaded or file is empty.");
                }
                if (csvFile.ContentType != "text/csv")
                {
                    return BadRequest("Uploaded file is not recognized as a csv file.");
                }

                using var memStream = new MemoryStream();

                await csvFile.CopyToAsync(memStream);

                memStream.Seek(0, SeekOrigin.Begin);

                using var reader = new StreamReader(memStream, Encoding.UTF8);

                List<IqxNetworkInstrument> results = [];


                bool checkToSync = true;
                while (!reader.EndOfStream)
                {
                    string? line = await reader.ReadLineAsync();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    var deserilized = line.Deserialize();
                    if (deserilized != default)
                    {
                        //Finally gets deserialized
                        //Check 2 things.
                        //1) Does the database contain the timestamp
                        //2) Does it contains the system
                        //If this can be found, refuse to do anything and return BadRequest
                        if (checkToSync)
                        {
                            //This block runs only on the first time.
                            report.Add(deserilized.System, 0);
                            var tryToFind = await _context.IqxInstrument.FirstOrDefaultAsync(
                                a =>
                                a.QueryTimeStamp == deserilized.QueryTimeStamp &&
                                a.System == deserilized.System);


                            if (tryToFind != null)
                            {
                                report[deserilized.System] = -1;
                                break;
                            }
                            else
                            {
                                checkToSync = false;
                            }
                        }

                        report[deserilized.System]++;
                        results.Add(deserilized);
                    }


                }

                await _context.IqxInstrument.AddRangeAsync(results);
                await _context.SaveChangesAsync();
            }

            return Ok(report);
        }

    }
}
