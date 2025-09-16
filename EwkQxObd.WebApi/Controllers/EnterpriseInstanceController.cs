using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EwkQxObd.WebApi.Controllers
{
    public class EnterpriseInstanceController : Controller
    {
        // GET: EnterpriseInstanceController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EnterpriseInstanceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EnterpriseInstanceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EnterpriseInstanceController/Create
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

        // GET: EnterpriseInstanceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EnterpriseInstanceController/Edit/5
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

        // GET: EnterpriseInstanceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EnterpriseInstanceController/Delete/5
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
