using EwkQxObd.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace EwkQxObd.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class TaskWorkFlowController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new EqoTaskWorkflow());
        }
    }
}
