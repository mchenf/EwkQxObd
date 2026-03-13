using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult New()
        {
            return View();
        }
    }
}
