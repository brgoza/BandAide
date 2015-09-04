using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace BandAide.Web.Models.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardViewModel(ApplicationUser user)
        {
            User = user;
        }

        [Key]
        public ApplicationUser User { get; set; }
        public List<Band> MemberOfBands => User.MemberOfBands;
        public List<NeedBandQuery> NeedBandQueries => User.NeedBandQueries;
        public string UserFullName => User.FullName;
        public string UserEmail => User.Email;
        public Instrument BestInstrument => User.InstrumentSkills.OrderByDescending(x => x.Proficiency).First().Instrument;


    }
}