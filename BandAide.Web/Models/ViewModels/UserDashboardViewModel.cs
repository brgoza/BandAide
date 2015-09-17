using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace BandAide.Web.Models.ViewModels
{
    public class UserDashboardViewModel
    {
        public UserDashboardViewModel(ApplicationUser user, ApplicationDbContext context)
        {
            UserId = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Image = user.ImageArray;
            InstrumentSkills = user.InstrumentSkills;
            MemberOfBands = user.MemberOfBands;
            NeedBandQueries = user.NeedBandQueries;
            MatchingQueries = new List<NeedMemberQuery>();

            foreach (var q in NeedBandQueries)
            {
                MatchingQueries.AddRange(context.NeedMemberQueriesDbSet.Where(x => x.Instrument.Id == q.Instrument.Id));
            }
        }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public byte[] Image { get; set; }
        public string Email { get; set; }
        public List<NeedMemberQuery> MatchingQueries { get; set; }

        public List<Band> MemberOfBands { get; set; }
        public List<NeedBandQuery> NeedBandQueries { get; set; }
        public List<InstrumentSkill> InstrumentSkills { get; set; }
        public Instrument BestInstrument => InstrumentSkills.OrderByDescending(x => x.Proficiency).First().Instrument;

        public Instrument SelectedInstrument { get; set; }

    }
}