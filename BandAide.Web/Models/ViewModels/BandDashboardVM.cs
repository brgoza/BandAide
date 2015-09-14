using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BandAide.Web.Models.ViewModels
{
    public class BandDashboardVM
    {
        public Guid BandId { get; set; }
        public string BandName { get; set; }
        public List<ApplicationUser> BandMembers { get; set; }
        public List<ApplicationUser> BandAdmins { get; set; }
        public List<NeedMemberQuery> NeedMemberQueries { get; set; } 
        public bool IsUserAdmin { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Age => Humanizer.TimeSpanHumanizeExtensions.Humanize(DateTime.Now - CreatedOn);
        public string NameOfUserToInvite { get; set; }
        public ApplicationUser UserToInvite { get; set; }

        public BandDashboardVM()
        {
            
        }
        public BandDashboardVM(Band band, bool userIsAdmin = false)
        {
            BandId = band.Id;
            CreatedOn = band.CreatedOn;
            BandName = band.Name;
            BandMembers = band.Members;
            BandAdmins = band.Admins;
            IsUserAdmin = userIsAdmin;
            NeedMemberQueries = band.NeedMemberQueries;
        }
     
    }

}