using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace BandAide.Web.Models
{
    public class Instrument
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public virtual List<ApplicationUser> ApplicationUsers { get; set; }

        public Instrument()
        {
            
        }
        public Instrument(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}