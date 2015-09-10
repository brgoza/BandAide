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
            UserDashboardViewModel userVM = new UserDashboardViewModel(GetCurrentUser());
            return View(userVM);
        }

        [Authorize]
        public ActionResult BandSearch()
        {
            BandSearchViewModel bandSearchVM = new BandSearchViewModel(GetCurrentUser());
            return View(bandSearchVM);
        }

        [Authorize]
        public ActionResult BandDashboard(Guid? bandId)
        {
            var band = _db.Bands.Find(bandId);
            var currentUser = GetCurrentUser();
            var isUserAdmin = band.Admins.Contains(currentUser);
            BandDashboardVM bandVM = new BandDashboardVM(_db.Bands.Find(bandId), isUserAdmin);
            return View(bandVM);
        }

        [Authorize]
        private ApplicationUser GetCurrentUser()
        {
            return _db.Users.Find(HttpContext.User.Identity.GetUserId());
        }
    }
}