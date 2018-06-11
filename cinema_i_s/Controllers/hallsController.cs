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
    public class hallsController : Controller
    {
        private cinema_inform_sistemEntities11 db = new cinema_inform_sistemEntities11();
      
        // GET: halls
     
        public ActionResult Index()
        {
            return View(db.hall.ToList());
        }
        [Authorize(Roles = "user")]
        public ActionResult Search(String searchText)
        {
            var result = db.hall
                .Where(h => h.name_hall.ToString().Contains(searchText.ToLower())
                    || h.capacity.ToString().Contains(searchText.ToLower()))
                .ToArray();
            return View(result);
        }
        [Authorize(Roles = "user")]
        // GET: halls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hall hall = db.hall.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            return View(hall);
        }
        [Authorize(Roles = "user")]
        // GET: halls/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "user")]
        // POST: halls/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name_hall,capacity,holl_id")] hall hall)
        {
            if (ModelState.IsValid)
            {
                db.hall.Add(hall);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hall);
        }
        [Authorize(Roles = "user")]
        // GET: halls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hall hall = db.hall.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            return View(hall);
        }
        [Authorize(Roles = "user")]
        // POST: halls/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "name_hall,capacity,holl_id")] hall hall)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hall).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hall);
        }
        [Authorize(Roles = "user")]
        // GET: halls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            hall hall = db.hall.Find(id);
            if (hall == null)
            {
                return HttpNotFound();
            }
            return View(hall);
        }
        [Authorize(Roles = "user")]
        // POST: halls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            hall hall = db.hall.Find(id);
            db.hall.Remove(hall);
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
