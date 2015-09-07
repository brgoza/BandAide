using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BandAide.Web.Models.ViewModels
{
    public class BandDashboardVM
    {
        [Key]
        public Band Band { get; set; }
        public bool IsUserAdmin { get; set; }

        public BandDashboardVM(Band band, bool userIsAdmin = false)
        {
            Band = band;
            IsUserAdmin = userIsAdmin;
        }
    }
  
}