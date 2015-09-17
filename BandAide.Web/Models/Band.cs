using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual List<NeedMemberQuery> NeedMemberQueries { get; set; }

        public static Band SaveNewBandToDB(string name, ApplicationUser founder, ApplicationDbContext context)
        {
            var newBand = new Band {Name = name};
            newBand.Admins.Add(founder);
            newBand.Members.Add(founder);
            newBand.CreatedOn = DateTime.Now;
            context.Bands.Add(newBand);
            context.SaveChanges();
            return newBand;

        }
        public bool AddMember(ApplicationUser user, ApplicationDbContext context)
        {
            if (Members.Contains(user)) return false;
            Members.Add(user);
            context.SaveChanges();
            return true;
        }
        
    }
}