using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
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
        public ActionResult AddMemberByName(Guid bandId)
        {
            var band = GetBandById(bandId);
            var vm = new AddMemberViewModel { Band = band };

            return View(vm);
        }

        

        public ActionResult AddMember(Guid bandId, string NameOfUserToInvite, Guid? instrumentId = null)
        {
            ApplicationUser invitee = _db.Users.FirstOrDefault(x => x.UserName == NameOfUserToInvite);
            var band = GetBandById(bandId);
            if (!band.Members.Contains(invitee))
            {
                GetBandById(bandId).AddMember(invitee, _db);
                }
            if (instrumentId != null)
            {
                DeactivateQuery(bandId, instrumentId);
            }
            return RedirectToAction("BandDashBoard", "Home", new { bandId = bandId });
        }

        [HttpGet]
        public ActionResult QueryByInstrument(Guid bandId)
        {
            var instruments = _db.Instruments.ToList();
            var band = GetBandById(bandId);
            var vm = new QueryByInstrumentViewModel(band, instruments);
            return View(vm);
        }

        [HttpPost]
        public ActionResult QueryByInstrument(Guid bandId, Guid selectedInstrumentId)
        {
            var instrument = _db.Instruments.Find(selectedInstrumentId);
            var band = _db.Bands.Find(bandId);
         var newQuery = new NeedMemberQuery(band, instrument) { Active = true };
            var results = new QueryForMembersResult(band, instrument, newQuery.ExecuteQuery(_db));

            if (band.NeedMemberQueries.All(x => x.Instrument.Id != newQuery.Instrument.Id))
            {
                _db.NeedMemberQueriesDbSet.Add(newQuery);
                _db.SaveChanges();
            }

            return View("QueryResults", results);
        }


        private bool DeactivateQuery(Guid bandId, Guid? instrumentId)
        {
            var row = _db.NeedMemberQueriesDbSet.FirstOrDefault(x => x.Band.Id == bandId && x.Instrument.Id == instrumentId);
            if (row == null)
            {
                return false;
            }
            row.Active = false;
            _db.SaveChanges();
            return true;
        }

        public Band GetBandById(Guid bandId)
        {
            return _db.Bands.Find(bandId);
        }

        public ApplicationUser GetUserById(Guid userId)
        {
            return _db.Users.Find(userId);
        }
     
    }
}
