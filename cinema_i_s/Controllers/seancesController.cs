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
    public class seancesController : Controller
    {
        private cinema_inform_sistemEntities11 db = new cinema_inform_sistemEntities11();
       
        // GET: seances
       
        public ActionResult Index()
        {
            var seance = db.seance.Include(s => s.associate).Include(s => s.film).Include(s => s.hall);
            return View(seance.ToList());
        }
        [Authorize(Roles = "user")]
        public ActionResult Search(String searchText)
        {
            var result = db.seance
                .Where(s => s.id_film.ToString().Contains(searchText.ToLower())
                    || s.id_assiciate.ToString().Contains(searchText.ToLower())
                    || s.id_hall.ToString().Contains(searchText.ToLower())
                    || s.price.ToString().Contains(searchText.ToLower())
                    || s.movie_format.ToString().Contains(searchText.ToLower())
                    || s.date.ToString().Contains(searchText.ToLower())
                    || s.time.ToString().Contains(searchText.ToLower()))
                .ToArray();
            return View(result);
        }
        [Authorize(Roles = "user")]
        // GET: seances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seance seance = db.seance.Find(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            return View(seance);
        }
        [Authorize(Roles = "user")]
        // GET: seances/Create
        public ActionResult Create()
        {
            ViewBag.id_assiciate = new SelectList(db.associate, "associate_id", "age");
            ViewBag.id_film = new SelectList(db.film, "film_id", "name_film");
            ViewBag.id_hall = new SelectList(db.hall, "holl_id", "name_hall");
            return View();
        }
        [Authorize(Roles = "user")]
        // POST: seances/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_film,id_assiciate,seance_id,id_hall,price,movie_format,date,time")] seance seance)
        {
            if (ModelState.IsValid)
            {
                db.seance.Add(seance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_assiciate = new SelectList(db.associate, "associate_id", "age", seance.id_assiciate);
            ViewBag.id_film = new SelectList(db.film, "film_id", "name_film", seance.id_film);
            ViewBag.id_hall = new SelectList(db.hall, "holl_id", "name_hall", seance.id_hall);
            return View(seance);
        }
        [Authorize(Roles = "user")]
        // GET: seances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seance seance = db.seance.Find(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_assiciate = new SelectList(db.associate, "associate_id", "age", seance.id_assiciate);
            ViewBag.id_film = new SelectList(db.film, "film_id", "name_film", seance.id_film);
            ViewBag.id_hall = new SelectList(db.hall, "holl_id", "name_hall", seance.id_hall);
            return View(seance);
        }
        [Authorize(Roles = "user")]
        // POST: seances/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_film,id_assiciate,seance_id,id_hall,price,movie_format,date,time")] seance seance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_assiciate = new SelectList(db.associate, "associate_id", "age", seance.id_assiciate);
            ViewBag.id_film = new SelectList(db.film, "film_id", "name_film", seance.id_film);
            ViewBag.id_hall = new SelectList(db.hall, "holl_id", "name_hall", seance.id_hall);
            return View(seance);
        }
        [Authorize(Roles = "user")]
        // GET: seances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seance seance = db.seance.Find(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            return View(seance);
        }
        [Authorize(Roles = "user")]
        // POST: seances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            seance seance = db.seance.Find(id);
            db.seance.Remove(seance);
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
