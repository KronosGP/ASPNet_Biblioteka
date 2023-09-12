using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Biblioteka.Models;

namespace Biblioteka.Controllers
{
    public class KsiazkiController:Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(bool? przeczytane)
        {
            IEnumerable<Ksiazka> query = db.Ksiazki;
            if (przeczytane.HasValue)
            {
                if (przeczytane.Value)
                    query = query.Where(k => k.DataPrzeczytania != null);
                else
                    query = query.Where(k => k.DataPrzeczytania == null);
            }
            var ksiazki = query.ToList();
            return View(ksiazki);
        }

        public ActionResult Create()
        {
            return PartialView("_PopUpBookPartial",new Ksiazka());
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Ksiazka ksiazka = db.Ksiazki.Find(id);
            if (ksiazka == null)
                return HttpNotFound();

            return PartialView("_PopUpBookPartial", ksiazka);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Ksiazka ksiazka = db.Ksiazki.Find(id);
                if (ksiazka == null)
                {
                    return Json(new { success = false });
                }

                db.Ksiazki.Remove(ksiazka);
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
        public ActionResult Save(Ksiazka ksiazka)
        {
            if (ModelState.IsValid)
            {
                if (ksiazka.Id == 0)
                    db.Ksiazki.Add(ksiazka);

                else
                    db.Entry(ksiazka).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                // Po zapisaniu książki możesz zwrócić odpowiedź JSON lub inny komunikat
                return Json(new { success = true});
            }

            // Jeśli wystąpiły błędy walidacji, zwróć odpowiedź JSON z błędami
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new { success = false, errors = errors });
        }
    }
}