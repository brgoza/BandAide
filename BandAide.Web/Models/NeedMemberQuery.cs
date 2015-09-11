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

        public Band Band { get; set; }
        public bool Active { get; set; }
        public DateTime QueryStartedOn { get; set; }
        public int HitCount { get; set; }
        public virtual List<Instrument> NeededInstruments { get; set; }
        public string QueryText { get; set; }
        public virtual List<Genre> PreferredGenres { get; set; }

        [NotMapped]
        public TimeSpan QueryAge => DateTime.Now - QueryStartedOn;
    }
}