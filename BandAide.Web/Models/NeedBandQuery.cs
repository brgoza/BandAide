using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BandAide.Web.Models
{
    public class NeedBandQuery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public ApplicationUser User { get; set; }
        public bool Active { get; set; }
        public DateTime QueryStartedOn { get; set; }
        public int HitCount { get; set; }
        public virtual List<Genre> PreferredGenres { get; set; }
        public string QueryText { get; set; }

        [NotMapped]
        public TimeSpan QueryAge => DateTime.Now - QueryStartedOn;

    }
}