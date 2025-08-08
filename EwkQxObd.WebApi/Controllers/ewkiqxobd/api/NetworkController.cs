using EwkQxObd.Core.Model;
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

            var data = await reader.ReadToEndAsync();

            //TODO: Need a way to deserialize this file format.
            throw new NotImplementedException();

            return Ok(data);
        }

    }
}
