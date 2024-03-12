using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Puc.SeuEmbarque.Presentation.Controllers
{
    public class PacotesController : Controller
    {
        // GET: PacotesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PacotesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PacotesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PacotesController/Create
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

        // GET: PacotesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PacotesController/Edit/5
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

        // GET: PacotesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PacotesController/Delete/5
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
