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
    public class associatesController : Controller
    {
        private cinema_inform_sistemEntities11 db = new cinema_inform_sistemEntities11();

        // GET: associates
       
        [AllowAnonymous]
        public ActionResult Index()
        {

            return View(db.associate.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [Authorize(Roles = "user")]
        public ActionResult Search(String searchText)
        {
            var result = db.associate
                .Include(a => a.ticket_selling)
                .Where(a => a.age.ToString().Contains(searchText.ToLower())
                    || a.full_name.ToString().Contains(searchText.ToLower())
                    || a.position.ToLower().Contains(searchText.ToLower()))
                     .ToArray();
            return View(result);
        }
        [Authorize(Roles = "user")]
        // GET: associates/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            associate associate = db.associate.Find(id);
            if (associate == null)
            {
                return HttpNotFound();
            }
            return View(associate);
        }

        // GET: associates/Create
        [Authorize(Roles = "user")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: associates/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "age,full_name,position,associate_id")] associate associate)
        {
            if (ModelState.IsValid)
            {
                db.associate.Add(associate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(associate);
        }

        // GET: associates/Edit/5
        [Authorize(Roles = "user")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            associate associate = db.associate.Find(id);
            if (associate == null)
            {
                return HttpNotFound();
            }
            return View(associate);
        }

        // POST: associates/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "user")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "age,full_name,position,associate_id")] associate associate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(associate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(associate);
        }
        [Authorize(Roles = "user")]
        // GET: associates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            associate associate = db.associate.Find(id);
            if (associate == null)
            {
                return HttpNotFound();
            }
            return View(associate);
        }
        [Authorize(Roles = "admin")]
        // POST: associates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            associate associate = db.associate.Find(id);
            db.associate.Remove(associate);
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
