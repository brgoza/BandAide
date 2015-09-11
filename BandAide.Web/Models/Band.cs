using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BandAide.Web.Models
{
    public class Band
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual List<ApplicationUser> Admins { get; set; }
        public virtual List<ApplicationUser> Members { get; set; }
        public virtual List<Genre> Genres { get; set; }

    }
}