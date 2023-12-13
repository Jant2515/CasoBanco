using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BancoPromericaCaso.Controllers
{
    public class usuarioController : Controller
    {
        // GET: usuarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: usuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: usuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: usuarioController/Create
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

        // GET: usuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: usuarioController/Edit/5
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

        // GET: usuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: usuarioController/Delete/5
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
