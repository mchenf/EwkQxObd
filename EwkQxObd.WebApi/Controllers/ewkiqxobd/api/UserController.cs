using EwkQxObd.Core.Model.Iqx;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/[controller]")]
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
        public async Task<IActionResult> Create([FromBody] User user)
        {
            _logger.LogInformation("Adding IQX User, Single");

            var userAdded = await _context.IqxUsers.AddAsync(user);

            _logger.LogInformation("Checking User Added:");
            _logger.LogInformation("State: {0}", userAdded.State.ToString());

            int changeSaved = 0;

            try
            {
                changeSaved = await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return Conflict(new
                {
                    ContentType = "application/json",
                    Message = e.InnerException is null ? e.Message : e.InnerException.Message
                });
            }

            _logger.LogInformation("Change Saved:");
            _logger.LogInformation(changeSaved.ToString());

            return Ok(new { ContentType = "application/json", Message = "New User Added" });
        }

        [HttpPost("[action]")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateBulk([FromBody] List<User> users)
        {
            _logger.LogInformation("Adding IQX User, Many");

            await _context.IqxUsers.AddRangeAsync(users);

            int changeSaved = 0;
            try
            {
                changeSaved = await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                return Conflict(new
                {
                    ContentType = "application/json",
                    Message = e.InnerException is null ? e.Message : e.InnerException.Message
                });
            }

            _logger.LogInformation("Change Saved:");
            _logger.LogInformation(changeSaved.ToString());

            return Ok(new { ContentType = "application/json", Message = "Many new Users Added" });
        }

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid UserId)
        {
            _logger.LogInformation("Delete IQX User");
            var UserToRemove = await _context.IqxUsers.FindAsync(UserId);

            _logger.LogInformation(UserToRemove?.Email);
            if (UserToRemove == null)
            {
                return NotFound();
            }

            var result = _context.IqxUsers.Remove(UserToRemove);

            await _context.SaveChangesAsync();

            return Ok(new { ContentType = "application/json", Message = "User removed.", Data = result.Entity } );
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User User)
        {
            var entity = await _context.IqxUsers.FindAsync(User.UserGuid);
            if (entity is null)
            {
                return NotFound();
            }

            var entityBefore = entity.ShallowCopy();


            entity.Email = User.Email;
            entity.UserGuid = User.UserGuid;
            entity.FirstName = User.FirstName;
            entity.LastName = User.LastName;
            entity.Locked = User.Locked;

            await _context.SaveChangesAsync();
            return Ok(new { ContentType = "application/json", Before = entityBefore, After = entity });
        }

    }
}
