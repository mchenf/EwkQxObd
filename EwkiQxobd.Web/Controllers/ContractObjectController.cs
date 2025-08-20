using EwkQxObd.WebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EwkiQxobd.Web.Controllers
{
    public class ContractObjectController : Controller
    {
        private EwkIqxObdContext _context;
        private ILogger<ContractObjectController> _logger;

        public ContractObjectController(EwkIqxObdContext ctx, ILogger<ContractObjectController> logger)
        {
            _context = ctx;
            _logger = logger;
        }
        // GET: ContractObjectController
        public async Task<ActionResult> Index()
        {
            return View(await _context.VwSysnetinstorg.ToListAsync());
        }

        // GET: ContractObjectController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContractObjectController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContractObjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContractObjectController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContractObjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContractObjectController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContractObjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
