using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BandAide.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;

        public byte[] ImageArray { get; set; }
        public DateTime? DOB { get; set; }
        public string Bio { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public virtual List<Band> MemberOfBands { get; set; }
        public virtual List<Band> AdminOfBands { get; set; }
        public virtual List<InstrumentSkill> InstrumentSkills { get; set; }
        public virtual List<Instrument> Instruments { get; set; }

        public virtual Instrument BestInstrument
        {

            get
            {
                return InstrumentSkills.FirstOrDefault(x => x.Proficiency == InstrumentSkills.Max(y => y.Proficiency))?.Instrument;
            }
        }

        public virtual List<NeedBandQuery> NeedBandQueries { get; set; }


        //[NotMapped]
        //public Image ProfileImage => Utility.ByteArrayToImage(ImageArray);


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}