using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly EwkIqxObdContext _context;

        public UserController(ILogger<UserController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var query = await _context.IqxUsers.ToListAsync();


            return Ok(new { ContentType = "application/json", data = query });
        }



        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> New([FromBody] IqxUser user)
        {
            _logger.LogInformation("Adding IQX User, Single");

            var userAdded = await _context.AddAsync(user);

            _logger.LogInformation("Checking User Added:");
            _logger.LogInformation("State: {0}", userAdded.State.ToString());

            var changeSaved = await _context.SaveChangesAsync();

            _logger.LogInformation("Change Saved:");
            _logger.LogInformation(changeSaved.ToString());

            return Ok(new { ContentType = "application/json", Message = "New User Added" });
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult NewMany([FromBody] List<IqxUser> users)
        {
            _logger.LogInformation("Adding IQX User, Many");


            return Ok(new { ContentType = "application/json", Message = "Many new Users Added" });
        }

    }
}
