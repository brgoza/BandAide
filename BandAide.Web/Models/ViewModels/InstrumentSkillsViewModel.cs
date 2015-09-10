using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandAide.Web.Models.ViewModels
{
    public class InstrumentSkillsViewModel
    {
        public ApplicationDbContext db { get; private set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public List<SelectListItem> AllInstruments { get; set; }
        public List<SelectListItem> Proficiencies { get; set; }
        public List<InstrumentSkill> InstrumentSkills { get; set; }
        public Guid SelectedInstrumentId { get; set; }
        public Proficiency SelectedProficiency { get; set; }
        public InstrumentSkillsViewModel()
        {

        }
        public InstrumentSkillsViewModel(ApplicationUser user, List<InstrumentSkill> skills, ApplicationDbContext context)
        {
            User = user;
            UserId = user.Id;
            InstrumentSkills = skills;
            var x = context.InstrumentsDbSet.ToList();
            AllInstruments = new List<SelectListItem>();
            foreach (Instrument i in context.InstrumentsDbSet)
            {
                AllInstruments.Add(new SelectListItem { Value = i.Id.ToString(), Text = i.Name });
            }

        }
    }
}