using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandAide.Web.Models.ViewModels
{
    public class NeedBandQueryViewModel
    {
        public ApplicationUser User { get; set; }

        public Instrument SelectedInstrument { get; set; }
        public SelectList InstrumentSelectList { get; set; }
        public SelectListItem SelectedInstrumentSelectListItem { get; set; }
        public Guid SelectedInstrumentId { get; set; }
        public List<Instrument> AllInstruments { get; private set; }

        public List<Band> SearchResults { get; set; }

        public NeedBandQueryViewModel(ApplicationUser user, List<Instrument> instruments)
        {
            User = user;
            InstrumentSelectList = new SelectList(instruments, "Id", "Name");
        }

        public NeedBandQueryViewModel(ApplicationUser user, Instrument instrument)
        {
            User = user;
            SelectedInstrument = instrument;
        }
    }
}