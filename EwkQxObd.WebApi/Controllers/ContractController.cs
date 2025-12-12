using EwkQxObd.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace EwkQxObd.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class ContractController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] EqoContract Contract)
        {
            if (!ModelState.IsValid)
            {
                return View(Contract);
            }

            throw new NotImplementedException();

        }



    }
}
