using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BandAide.Web.Models
{
    public class NeedBandQueryViewModel
    {
        public ApplicationUser User { get; set; }
        public InstrumentSkill PreferredInstrument { get; set; }

        public List<Band> SearchResults { get; set; }

        public NeedBandQueryViewModel(ApplicationUser user)
        {
                     User = user;
        }
    }
}