using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BandAide.Web.Models;
using Microsoft.AspNet.Identity;
using BandAide.Web.Models.ViewModels;

namespace BandAide.Web.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(Guid userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.FirstOrDefault(x => x.Id == userId.ToString());
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }


        public ActionResult UserProfile(Guid Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(Id.ToString());
            if (user.Id != HttpContext.User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile(ApplicationUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult ModSkills(Guid? id)
        {
            var user = db.Users.Find(id.ToString());
            InstrumentSkillsViewModel vm = new InstrumentSkillsViewModel(user,user.InstrumentSkills,db);
            return View(vm);
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult ModSkills(InstrumentSkillsViewModel skillsVM)
        {
            var user = db.Users.Find(skillsVM.UserId);
            InstrumentSkill newSkill = new InstrumentSkill { ApplicationUser = user, Instrument = db.InstrumentsDbSet.Find(skillsVM.SelectedInstrumentId), Proficiency = skillsVM.SelectedProficiency};
            user.InstrumentSkills.Add(newSkill);
            db.SaveChanges();
            return RedirectToAction("ModSkills","Users",user);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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
