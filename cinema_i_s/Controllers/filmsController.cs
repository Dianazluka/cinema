using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cinema_i_s.Models;

namespace cinema_i_s.Controllers
{
    [Authorize]
    public class filmsController : Controller
    {
        private cinema_inform_sistemEntities11 db = new cinema_inform_sistemEntities11();
        
        // GET: films
        public ActionResult Index()
        {
            return View(db.film.ToList());
        }
        [Authorize(Roles = "user")]
        public ActionResult Search(String searchText)
        {
            var result = db.film
                .Where(f => f.name_film.ToString().Contains(searchText.ToLower())
                    || f.producer.ToString().Contains(searchText.ToLower())
                    || f.genre.ToLower().Contains(searchText.ToLower())
                    || f.year.ToString().Contains(searchText.ToLower())
                    || f.age_limit.ToLower().Contains(searchText.ToLower()))
                .ToArray();
            return View(result);
        }
        [Authorize(Roles = "user")]
        // GET: films/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            film film = db.film.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }
        [Authorize(Roles = "user")]
        // GET: films/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "user")]
        // POST: films/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name_film,producer,genre,year,age_limit,film_id")] film film)
        {
            if (ModelState.IsValid)
            {
                db.film.Add(film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film);
        }
        [Authorize(Roles = "user")]
        // GET: films/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            film film = db.film.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }
        [Authorize(Roles = "user")]
        // POST: films/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name_film,producer,genre,year,age_limit,film_id")] film film)
        {
            if (ModelState.IsValid)
            {
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(film);
        }
        [Authorize(Roles = "user")]
        // GET: films/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            film film = db.film.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }
        [Authorize(Roles = "user")]
        // POST: films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            film film = db.film.Find(id);
            db.film.Remove(film);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "user")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
