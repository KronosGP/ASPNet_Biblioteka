using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Biblioteka.Models;

namespace Biblioteka.Controllers
{
    public class FilmyController : Controller
    {
        // GET: Filmy
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(bool? obejrzane)
        {
            IEnumerable<Filmy> query = db.Filmy;
            if (obejrzane.HasValue)
            {
                if (obejrzane.Value)
                    query = query.Where(k => k.DataObejrzenia != null);
                else
                    query = query.Where(k => k.DataObejrzenia == null);
            }
            var filmy = query.ToList();
            return View(filmy);
        }

        public ActionResult Create()
        {
            return PartialView("_PopUpBookPartial", new Filmy());
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Filmy film = db.Filmy.Find(id);
            if (film == null)
                return HttpNotFound();

            return PartialView("_PopUpBookPartial", film);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Filmy film = db.Filmy.Find(id);
                if (film == null)
                {
                    return Json(new { success = false });
                }

                db.Filmy.Remove(film);
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Obsłuż błąd
                return Json(new { success = false, error = ex.Message });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Filmy film)
        {
            if (ModelState.IsValid)
            {
                if (film.Id == 0)
                    db.Filmy.Add(film);

                else
                    db.Entry(film).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                // Po zapisaniu książki możesz zwrócić odpowiedź JSON lub inny komunikat
                return Json(new { success = true });
            }

            // Jeśli wystąpiły błędy walidacji, zwróć odpowiedź JSON z błędami
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, errors = errors });
        }
    }
}