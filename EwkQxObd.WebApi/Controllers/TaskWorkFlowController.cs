using EwkQxObd.Core.Model;
using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EwkQxObd.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class TaskWorkFlowController(EwkIqxObdContext context, ILogger<TaskWorkFlowController> logger) : Controller
    {
        private const string TwfCreateMsg = "Added TWF: {result}";
        private readonly EwkIqxObdContext _context = context;
        private readonly ILogger<TaskWorkFlowController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Results = await _context.TaskWorkFlow.ToListAsync();
            return View(Results);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new EqoTaskWorkflow());
        }

        [HttpPost]
        public async Task<IActionResult> Create(EqoTaskWorkflow WorkFlow)
        {
            _logger.BeginScope("Create()");
            var twf = await _context.TaskWorkFlow.AddAsync(WorkFlow);
            _logger.LogDebug(TwfCreateMsg, twf);

            var result = await _context.SaveChangesAsync();

            _logger.LogDebug(TwfCreateMsg, result);

            return RedirectToAction(nameof(Index));

        }
    }
}
