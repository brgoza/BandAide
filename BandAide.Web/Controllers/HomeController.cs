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

        private ApplicationUser _currentUser => _db.Users.Find(HttpContext.User.Identity.GetUserId());

        public ActionResult Index()
        {
            if (Request.IsAuthenticated) return View("Dashboard", new DashboardViewModel(_currentUser));
            return View();
        }



    }
}