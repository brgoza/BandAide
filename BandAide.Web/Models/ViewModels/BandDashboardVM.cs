using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BandAide.Web.Models.ViewModels
{
    public class BandDashboardVM
    {
     
        public string BandName { get; set; }
        public List<ApplicationUser> BandMembers { get; set; }
        public List<ApplicationUser> BandAdmins { get; set; }
        public bool IsUserAdmin { get; set; }
        public DateTime CreatedOn { get; set; }
        public BandDashboardVM(Band band, bool userIsAdmin = false)
        {
            BandName = band.Name;
            BandMembers = band.Members;
            BandAdmins = band.Admins;
            IsUserAdmin = userIsAdmin;
        }
     
    }

}