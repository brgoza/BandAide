using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BandAide.Web.Models
{
    public class BandSearchViewModel
    {
        public Guid Id { get; set; }
        public ApplicationUser User { get; set; }
        public InstrumentSkill PreferredInstrument { get; set; }

        public List<Band> SearchResults { get; set; }

        public BandSearchViewModel(ApplicationUser user)
        {
            Id = Guid.NewGuid();
            User = user;
        }
    }
}