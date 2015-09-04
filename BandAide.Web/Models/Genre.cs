using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BandAide.Web.Models
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<Band> Bands { get; set; }
    }
}