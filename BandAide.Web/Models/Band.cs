using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BandAide.Web.Models
{
    public class Band
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public ApplicationUser Creator { get; set; }
       
        public virtual List<ApplicationUser> Members { get; set; }
        public virtual List<Genre> Genres { get; set; }

    }
}