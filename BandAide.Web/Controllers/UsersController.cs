using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BandAide.Web.Models;
using BandAide.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace BandAide.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Details(Guid userId)
        {
            var applicationUser = _db.Users.FirstOrDefault(x => x.Id == userId.ToString());
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        public ActionResult UserProfile(Guid Id)
        {
            var user = _db.Users.Find(Id.ToString());
            if (user.Id != HttpContext.User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile(ApplicationUser user)
        {
            if (!ModelState.IsValid) return View(user);
            _db.Entry(user).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ModSkills(Guid? id)
        {
            var user = GetCurrentUser();
            var vm = new InstrumentSkillsViewModel(user, user.InstrumentSkills, _db);
            return View(vm);
        }

      public ActionResult AddSkill(Guid selectedInstrumentId, Proficiency proficiency)
        {
            var user = GetCurrentUser();
            var newSkill = new InstrumentSkill
            {
                ApplicationUser = user,
                Instrument = _db.Instruments.Find(selectedInstrumentId),
                Proficiency = (Proficiency)proficiency
            };
            user.InstrumentSkills.Add(newSkill);
            _db.SaveChanges();
            return RedirectToAction("ModSkills", "Users", user);
        }

        [HttpGet]
        public ActionResult QueryForBand(string userId)
        {
            var user = _db.Users.Find(userId);
            if (user.InstrumentSkills.Count == 0)
            {
                return RedirectToAction("ModSkills", new {id = userId});
            }
            var vm = new NeedBandQueryViewModel(user);
            return View(vm);
        }

        [HttpPost]
        public ActionResult QueryForBand(string userId, Guid selectedInstrumentId)
        {
            var instrument = _db.Instruments.Find(selectedInstrumentId);
            var user = _db.Users.Find(userId);
            var newQuery = new NeedBandQuery(user, instrument) {Active = true};
            var results = newQuery.ExecuteQuery(_db);
            if (!user.NeedBandQueries.Any(x => x.Instrument.Id == newQuery.Instrument.Id))
            {
                _db.NeedBandQueriesDbSet.Add(newQuery);
                _db.SaveChanges();
            }
            return View("QueryResults", results);
        }

        public Band GetBandById(Guid bandId)
        {
            return _db.Bands.Find(bandId);
        }

        public ApplicationUser GetCurrentUser()
        {
            return _db.Users.Find(HttpContext.User.Identity.GetUserId());
        }
        public ApplicationUser GetUserById(Guid userId)
        {
            return _db.Users.Find(userId);
        }
        }
}