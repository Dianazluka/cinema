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
    public class ticketsController : Controller
    {
        private cinema_inform_sistemEntities11 db = new cinema_inform_sistemEntities11();
       
        // GET: tickets
        public ActionResult Index()
        {
            var ticket = db.ticket.Include(t => t.seance);
            return View(ticket.ToList());
        }
        [Authorize(Roles = "user")]
        public ActionResult Search(String searchText)
        {
            var result = db.ticket
                .Where(t => t.seance_id.ToString().Contains(searchText.ToLower())
                    || t.row.ToString().Contains(searchText.ToLower())
                    || t.place.ToString().Contains(searchText.ToLower()))
                .ToArray();
            return View(result);
        }
        [Authorize(Roles = "user")]
        // GET: tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket ticket = db.ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: tickets/Create
        public ActionResult Create()
        {
            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "price");
            return View();
        }

        // POST: tickets/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "seance_id,row,place,ticket_id")] ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.ticket.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "price", ticket.seance_id);
            return View(ticket);
        }

        // GET: tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket ticket = db.ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "price", ticket.seance_id);
            return View(ticket);
        }

        // POST: tickets/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "seance_id,row,place,ticket_id")] ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "price", ticket.seance_id);
            return View(ticket);
        }

        // GET: tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket ticket = db.ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ticket ticket = db.ticket.Find(id);
            db.ticket.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
