using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BandAide.Web.Models;
using Microsoft.AspNet.Identity;

namespace BandAide.Web.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            //var usr = db.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (Request.IsAuthenticated) 
            return View("About");
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}