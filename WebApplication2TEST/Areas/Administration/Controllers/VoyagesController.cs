using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2TEST.Models;

namespace WebApplication2TEST.Areas.Administration.Controllers
{
    public class VoyagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administration/Voyages
        public ActionResult Index()
        {
            var voyages = db.Voyages.Include(v => v.AgenceVoyage).Include(v => v.Destination);
            return View(voyages.ToList());
        }

        // GET: Administration/Voyages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);
        }

        // GET: Administration/Voyages/Create
        public ActionResult Create()
        {
            ViewBag.AgenceVoyageID = new SelectList(db.AgencesVoyages, "ID", "Nom");
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "Continent");
            return View();
        }

        // POST: Administration/Voyages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateAller,DateRetour,PlacesDisponibles,PrixParPersonne,AgenceVoyageID,DestinationID")] Voyage voyage)
        {
            if (ModelState.IsValid)
            {
                db.Voyages.Add(voyage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgenceVoyageID = new SelectList(db.AgencesVoyages, "ID", "Nom", voyage.AgenceVoyageID);
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "Continent", voyage.DestinationID);
            return View(voyage);
        }

        // GET: Administration/Voyages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgenceVoyageID = new SelectList(db.AgencesVoyages, "ID", "Nom", voyage.AgenceVoyageID);
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "Continent", voyage.DestinationID);
            return View(voyage);
        }

        // POST: Administration/Voyages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateAller,DateRetour,PlacesDisponibles,PrixParPersonne,AgenceVoyageID,DestinationID")] Voyage voyage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voyage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgenceVoyageID = new SelectList(db.AgencesVoyages, "ID", "Nom", voyage.AgenceVoyageID);
            ViewBag.DestinationID = new SelectList(db.Destinations, "ID", "Continent", voyage.DestinationID);
            return View(voyage);
        }

        // GET: Administration/Voyages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return HttpNotFound();
            }
            return View(voyage);
        }

        // POST: Administration/Voyages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            db.Voyages.Remove(voyage);
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
