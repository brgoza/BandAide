﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BandAide.Web.Models;

namespace BandAide.Web.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
       
        public ActionResult FixUserErrors()
        {
            Utility.FilterToArkansas(db);
            return RedirectToAction("Index", "Home");
        }
    }
}