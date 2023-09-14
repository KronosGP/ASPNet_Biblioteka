using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Biblioteka.Models;

namespace Biblioteka.Controllers
{
    public class SerialeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(bool? obejrzane)
        {
            IEnumerable<Seriale> query = db.Seriale;
            if (obejrzane.HasValue)
            {
                if (obejrzane.Value)
                    query = query.Where(k => k.DataObejrzenia != null);
                else
                    query = query.Where(k => k.DataObejrzenia == null);
            }
            var seriale = query.ToList();
            return View(seriale);
        }

        public ActionResult Create()
        {
            return PartialView("_PopUpBookPartial", new Seriale());
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Seriale serial = db.Seriale.Find(id);
            if (serial == null)
                return HttpNotFound();

            return PartialView("_PopUpBookPartial", serial);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Seriale serial = db.Seriale.Find(id);
                if (serial == null)
                {
                    return Json(new { success = false });
                }

                db.Seriale.Remove(serial);
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
        public ActionResult Save(Seriale serial)
        {
            if (ModelState.IsValid)
            {
                if (serial.Id == 0)
                    db.Seriale.Add(serial);

                else
                    db.Entry(serial).State = System.Data.Entity.EntityState.Modified;

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