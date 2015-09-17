using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BandAide.Web.Models;
using BandAide.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace BandAide.Web.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated) return RedirectToAction("UserDashboard", "Home");
            return View();
        }

        [Authorize]
        public ActionResult UserDashBoard()
        {
            var userVM = new UserDashboardViewModel(GetCurrentUser(),_db);
            return View(userVM);
        }
        
        [Authorize]
        public ActionResult BandDashboard(Guid? bandId)
        {
            var currentUser = GetCurrentUser();
            var band = _db.Bands.Find(bandId);

            if (currentUser == null || band == null) return View("Index");
            var isUserAdmin = band.Admins.Contains(currentUser);
            var bandVM = new BandDashboardVM(_db.Bands.Find(bandId), isUserAdmin);
            return View(bandVM);
        }

        private ApplicationUser GetCurrentUser()
        {
            return _db.Users.Find(HttpContext.User.Identity.GetUserId());
        }
    }
}