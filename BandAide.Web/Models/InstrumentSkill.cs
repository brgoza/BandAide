﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BandAide.Web.Models
{
    public enum Proficiency
    {
        Novice,
        NoviceIntermediate,
        Intermediate,
        IntermediatePro,
        Pro,
        Virtuoso
    }

    public class InstrumentSkill
    {
        //constructors
        public InstrumentSkill()
        {
            Id = Guid.NewGuid();
        }
        public InstrumentSkill(Instrument instrument, Proficiency proficiency, string background,
            ApplicationUser applicationUser)
        {
            Id = Guid.NewGuid();
            ApplicationUser = applicationUser;
            Instrument = instrument;
            Proficiency = proficiency;
            BackgroundDescription = background;
        }

        [Key]
        public Guid Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public virtual Instrument Instrument { get; set; }
        public Proficiency Proficiency { get; set; }
        public string BackgroundDescription { get; set; }
    }
}