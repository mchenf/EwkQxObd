
using EwkQxObd.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.api
{
    [ApiController]
    [Route("ewkiqxobd/api/[controller]")]
    public class OrganisationController : Controller
    {
        private readonly ILogger<OrganisationController> _logger;

        public OrganisationController(ILogger<OrganisationController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<IqxOrganization> Get()
        {
            var result = new List<IqxOrganization>();

            result.Add(new IqxOrganization
            {
                GeisGuid = Guid.NewGuid(),
                Name = "Test",
                City = "Test1",
            });

            return result;
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult CreateOrganization(IqxOrganization org)
        {
            return Ok(new { Consumes = "application/json", Values = org });
        }
    }
}
