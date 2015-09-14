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
        public virtual List<Genre> Genres { get; set; }
        public virtual List<NeedMemberQuery> MemberQueries { get; set; }

        public Boolean AddMember(ApplicationUser user, ApplicationDbContext context)
        {
            this.Members.Add(user);
            context.SaveChanges();
            return true;
        }
        public NeedMemberQuery CreateNeedMemberQuery(List<Instrument> neededInstruments, string queryText)
        {
            return new NeedMemberQuery
            {
                Active = true,
                Band = this,
                NeededInstruments = neededInstruments,
                QueryText = queryText
            };
        }
    }
}