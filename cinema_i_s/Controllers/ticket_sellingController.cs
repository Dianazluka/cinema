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
    public class ticket_sellingController : Controller
    {
        private cinema_inform_sistemEntities11 db = new cinema_inform_sistemEntities11();

        // GET: ticket_selling
      
        public ActionResult Index()
        {
            var ticket_selling = db.ticket_selling.Include(t => t.associate).Include(t => t.ticket);
            return View(ticket_selling.ToList());
        }
        [Authorize(Roles = "user")]
        public ActionResult Search(String searchText)
        {
            var result = db.ticket_selling
                .Where(ts => ts.id_ticket.ToString().Contains(searchText.ToLower())
                    || ts.date_of_sale.ToString().Contains(searchText.ToLower())
                    || ts.associate_id.ToString().Contains(searchText.ToLower()))
                .ToArray();
            return View(result);
        }
        [Authorize(Roles = "user")]
        // GET: ticket_selling/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket_selling ticket_selling = db.ticket_selling.Find(id);
            if (ticket_selling == null)
            {
                return HttpNotFound();
            }
            return View(ticket_selling);
        }

        // GET: ticket_selling/Create
        public ActionResult Create()
        {
            ViewBag.associate_id = new SelectList(db.associate, "associate_id", "age");
            ViewBag.id_ticket = new SelectList(db.ticket, "ticket_id", "row");
            return View();
        }

        // POST: ticket_selling/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_ticket,date_of_sale,associate_id,ticket_selling_id")] ticket_selling ticket_selling)
        {
            if (ModelState.IsValid)
            {
                db.ticket_selling.Add(ticket_selling);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.associate_id = new SelectList(db.associate, "associate_id", "age", ticket_selling.associate_id);
            ViewBag.id_ticket = new SelectList(db.ticket, "ticket_id", "row", ticket_selling.id_ticket);
            return View(ticket_selling);
        }

        // GET: ticket_selling/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket_selling ticket_selling = db.ticket_selling.Find(id);
            if (ticket_selling == null)
            {
                return HttpNotFound();
            }
            ViewBag.associate_id = new SelectList(db.associate, "associate_id", "age", ticket_selling.associate_id);
            ViewBag.id_ticket = new SelectList(db.ticket, "ticket_id", "row", ticket_selling.id_ticket);
            return View(ticket_selling);
        }

        // POST: ticket_selling/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_ticket,date_of_sale,associate_id,ticket_selling_id")] ticket_selling ticket_selling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket_selling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.associate_id = new SelectList(db.associate, "associate_id", "age", ticket_selling.associate_id);
            ViewBag.id_ticket = new SelectList(db.ticket, "ticket_id", "row", ticket_selling.id_ticket);
            return View(ticket_selling);
        }

        // GET: ticket_selling/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ticket_selling ticket_selling = db.ticket_selling.Find(id);
            if (ticket_selling == null)
            {
                return HttpNotFound();
            }
            return View(ticket_selling);
        }

        // POST: ticket_selling/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ticket_selling ticket_selling = db.ticket_selling.Find(id);
            db.ticket_selling.Remove(ticket_selling);
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
