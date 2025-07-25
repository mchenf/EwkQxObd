
using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/[controller]")]
    public class OrganisationController : Controller
    {
        private readonly ILogger<OrganisationController> _logger;
        private readonly EwkIqxObdContext _context;

        public OrganisationController(ILogger<OrganisationController> logger, EwkIqxObdContext dataContext)
        {
            _logger = logger;
            _context = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<IqxOrganization> Get()
        {
            return _context.IqxOrganisation;
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult CreateOrganization(IqxOrganization org)
        {
            return Ok(new { Consumes = "application/json", Values = org });
        }
    }
}
