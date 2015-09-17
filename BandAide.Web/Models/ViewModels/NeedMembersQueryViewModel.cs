using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BandAide.Web.Models.ViewModels
{
    public class NeedMembersQueryViewModel
    {
        public Band Band { get; set; }

        public Instrument SelectedInstrument { get; set; }
        public SelectList InstrumentSelectList { get; set; }
        public SelectListItem SelectedInstrumentSelectListItem { get; set; }
        public Guid SelectedInstrumentId { get; set; }
        public List<Instrument> AllInstruments { get; private set; }
        public List<ApplicationUser> Members => Band.Members;
        public List<ApplicationUser> SearchResults { get; set; }

        public NeedMembersQueryViewModel(Band band, List<Instrument> instruments)
        {
            InstrumentSelectList = new SelectList(instruments, "Id", "Name");
            Band = band;
        }

        public NeedMembersQueryViewModel(Band band, Instrument instrument)
        {
            Band = band;
            SelectedInstrument = instrument;
        }
    }

}
