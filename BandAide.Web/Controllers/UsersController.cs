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
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            return View(_db.Users.ToList());
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(Guid userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = _db.Users.FirstOrDefault(x => x.Id == userId.ToString());
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
            ApplicationUser user = _db.Users.Find(Id.ToString());
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
            if (ModelState.IsValid)
            {
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }



        [System.Web.Mvc.HttpGet]
        public ActionResult ModSkills(Guid? id)
        {
            var user = _db.Users.Find(id.ToString());
            InstrumentSkillsViewModel vm = new InstrumentSkillsViewModel(user, user.InstrumentSkills, _db);
            return View(vm);
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult ModSkills(InstrumentSkillsViewModel skillsVM)
        {
            var user = _db.Users.Find(skillsVM.UserId);
            InstrumentSkill newSkill = new InstrumentSkill { ApplicationUser = user, Instrument = _db.InstrumentsDbSet.Find(skillsVM.SelectedInstrumentId), Proficiency = skillsVM.SelectedProficiency };
            user.InstrumentSkills.Add(newSkill);
            _db.SaveChanges();
            return RedirectToAction("ModSkills", "Users", user);
        }
        [System.Web.Mvc.HttpGet]
        public ActionResult QueryForBand(string userId)
        {
            var user = _db.Users.Find(userId);
            var instruments = _db.InstrumentsDbSet.ToList();
            var vm = new NeedBandQueryViewModel(user,instruments);
            return View(vm);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult QueryForBand(string userId, Guid selectedInstrumentId)
        {
            var instrument = _db.InstrumentsDbSet.Find(selectedInstrumentId);
            var user = _db.Users.Find(userId);
            var vm = new NeedBandQueryViewModel(user, instrument);
            var newQuery = new NeedBandQuery(user, instrument);
            newQuery.Active = true;
            var results = newQuery.ExecuteQuery(_db);
            _db.NeedBandQueriesDbSet.Add(newQuery);
            _db.SaveChanges();

            return View("QueryResults", results);
        }

        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = _db.Users.Find(id);
            _db.Users.Remove(applicationUser);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public Band GetBandById(Guid bandId)
        {
            return _db.Bands.Find(bandId);
        }

        public ApplicationUser GetUserById(Guid userId)
        {
            return _db.Users.Find(userId);
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
