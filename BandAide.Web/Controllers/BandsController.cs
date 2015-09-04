using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BandAide.Web.Models;
using Microsoft.AspNet.Identity;

namespace BandAide.Web.Controllers
{
    public class BandsController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
       
        // GET: Bands
        public ActionResult Index()
        {
            return View(_db.Bands.ToList());
        }

        // GET: Bands/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Band band = _db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        // GET: Bands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Band band)
        {
            if (!ModelState.IsValid) return View(band);

            band.Id = Guid.NewGuid();
            var currentUserId = User.Identity.GetUserId();
            var currentUser = _db.Users.Find(currentUserId);
            band.Admins = new List<ApplicationUser> {currentUser};
            band.Members = new List<ApplicationUser> {currentUser};
            band.CreatedOn = DateTime.Now;

            _db.Bands.Add(band);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Bands/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Band band = _db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        // POST: Bands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedOn")] Band band)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(band).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(band);
        }

        // GET: Bands/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Band band = _db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        // POST: Bands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Band band = _db.Bands.Find(id);
            _db.Bands.Remove(band);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
