using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BandAide.Web.Models;
using BandAide.Web.Models.ViewModels;
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


        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Band band)
        {
            if (!ModelState.IsValid) return View(band);

            var currentUser = _db.Users.Find(User.Identity.GetUserId());
            band.Admins = new List<ApplicationUser> { currentUser };
            band.Members = new List<ApplicationUser> { currentUser };
            band.CreatedOn = DateTime.Now;

            _db.Bands.Add(band);
            _db.SaveChanges();

            return RedirectToAction("BandDashBoard", "Home", new { bandId = band.Id });
        }

        [HttpGet]
        public ActionResult AddMember(Guid bandId)
        {
            var band = GetBandById(bandId);
            var vm = new AddMemberViewModel { Band = band };
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddMember(Guid bandId, string NameOfUserToInvite)
        {
            ApplicationUser invitee = _db.Users.FirstOrDefault(x => x.UserName == NameOfUserToInvite);
            GetBandById(bandId).AddMember(invitee, _db);
            return RedirectToAction("BandDashBoard", "Home", new { bandId = bandId });
        }

        [HttpGet]
        public ActionResult QueryByInstrument(Guid bandId)
        {
            var band = GetBandById(bandId);
            var vm = new QueryByInstrumentViewModel(band, _db);
            return View(vm);
        }

        [HttpPost]
        public ActionResult QueryByInstrument(Guid bandId, Guid instrumentId)
        {
            var results = new QueryByInstrumentViewModel(GetBandById(bandId), Instrument.GetById(instrumentId, _db), _db);
            var newQuery = new NeedMemberQuery();
            newQuery.Active = true;
            newQuery.Band = results.Band;
            newQuery.NeededInstruments = new List<Instrument>();
            newQuery.NeededInstruments.Add(Instrument.GetById(instrumentId, _db));
            newQuery.QueryStartedOn = DateTime.Now;
            _db.NeedMemberQueriesDbSet.Add(newQuery);
            _db.SaveChanges();
            return View("QueryResults", results.SearchResults);
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
