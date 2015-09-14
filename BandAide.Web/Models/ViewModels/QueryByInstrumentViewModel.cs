using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BandAide.Web.Models.ViewModels
{
    public class QueryByInstrumentViewModel
    {
        public Band Band { get; set; }
        public Guid BandId { get; set; }
        public Instrument InstrumentToSearchFor { get; set; }

        public SelectList InstrumentSelectList { get; set; }
        public SelectListItem SelectedInstrumentSelectListItem { get; set; }
        public Guid InstrumentId { get; set; }
        public List<Instrument> AllInstruments { get; private set; }
        public List<ApplicationUser> Members => Band.Members;
        public List<ApplicationUser> SearchResults { get; set; }

        public QueryByInstrumentViewModel(Band band, ApplicationDbContext context)
        {
            AllInstruments = context.InstrumentsDbSet.ToList();
            InstrumentSelectList = new SelectList(AllInstruments, "Id", "Name");
            Band = band;
        }

        public QueryByInstrumentViewModel(Band band, Instrument instrument, ApplicationDbContext context)
        {
            Band = band;
            BandId = band.Id;
            AllInstruments = context.InstrumentsDbSet.ToList();
            InstrumentSelectList = new SelectList(AllInstruments, "Id", "Name");
            InstrumentToSearchFor = instrument;
            InstrumentId = InstrumentToSearchFor.Id;
            SearchResults = context.Users.Where(x => x.InstrumentSkills.Any(y => y.Instrument.Id == InstrumentId)).ToList();
        }

        //public QueryByInstrumentViewModel(Band band, Guid instrumentId, ApplicationDbContext context)
        //{
        //    Band = band;
        //    AllInstruments = context.InstrumentsDbSet.ToList();
        //    InstrumentToSearchFor = Instrument.GetById(instrumentId, context);
        //    SearchResults = context.Users.Where(x => x.InstrumentSkills.Any(y => y.Instrument == InstrumentToSearchFor)).ToList();
        //}
    }

}
