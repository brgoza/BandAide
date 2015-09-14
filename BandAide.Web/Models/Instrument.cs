using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BandAide.Web.Models
{
    public class Instrument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Instrument()
        {

        }
        public Instrument(string name)
        {
            Name = name;
        }

        public static Instrument GetById(Guid id,ApplicationDbContext context)
        {
            return context.InstrumentsDbSet.Find(id);
        }
    }
}