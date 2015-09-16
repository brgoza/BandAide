using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BandAide.Web.Models
{
    public class Invite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool Active { get; set; }
        public Band Band { get; set; }

        public virtual ApplicationUser Inviter { get; set; }
        public virtual ApplicationUser Invitee { get; set; }

        public Invite(ApplicationUser inviter, ApplicationUser invitee)
        {
            Inviter = inviter; Invitee = invitee;
        }
    }
}