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


        [HttpPost("csv/upload")]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        public async Task<IActionResult> UploadCsv(IFormFile csvFile)
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

            
            List<IqxNetworkInstrument> results = new();

            while (!reader.EndOfStream)
            {
                string? line = await reader.ReadLineAsync();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                var deserilized = line.Deserialize();
                bool checkToSync = true;
                if (deserilized != default)
                {
                    //Finally gets deserialized
                    //Check 2 things.
                    //1) Does the database contain the timestamp
                    //2) Does it contains the system
                    //If this can be found, refuse to do anything and return BadRequest
                    if (checkToSync)
                    {

                        var tryToFind = await _context.IqxInstrument.FirstOrDefaultAsync(
                            a =>
                            a.QueryTimeStamp == deserilized.QueryTimeStamp &&
                            a.System == deserilized.System);

                        if (tryToFind != null)
                        {
                            return BadRequest(
                                "File is already sycned with duplicated query time & system." +
                                $"Query Time: {tryToFind.QueryTimeStamp};" +
                                $"System: {tryToFind.System};" +
                                $"Try to upload a different query csv."
                            );
                        }
                        else
                        {
                            checkToSync = false;
                        }
                    }


                    results.Add(deserilized);
                }


            }

            await _context.IqxInstrument.AddRangeAsync(results);

            await _context.SaveChangesAsync();

            return Ok(results);
        }

    }
}
