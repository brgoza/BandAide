using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BandAide.Web.Models
{
    public class NeedMemberQuery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public virtual Band Band { get; set; }
        public bool Active { get; set; }
        public DateTime QueryStartedOn { get; set; }
        public int HitCount { get; set; }
        public virtual Instrument Instrument { get; set; }
        public string QueryText { get; set; }
        public virtual List<Genre> PreferredGenres { get; set; }

        [NotMapped]
        public TimeSpan QueryAge => DateTime.Now - QueryStartedOn;

        public NeedMemberQuery()
        {

        }
        public NeedMemberQuery(Band band, Instrument instrument)
        {
            Band = band; Instrument = instrument; QueryStartedOn = DateTime.Now;
        }

        public List<ApplicationUser> ExecuteQuery(ApplicationDbContext context)
        {
            var matches = context.NeedBandQueriesDbSet.Where(x => x.Instrument.Id == Instrument.Id).ToList();
            List<ApplicationUser> results = new List<ApplicationUser>();
            matches.ForEach(x => results.Add(x.User));
            return results;
        }
    }
}