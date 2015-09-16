using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BandAide.Web.Models.ViewModels
{
    public class AddMemberViewModel
    {
        public Band Band { get; set; }
        public ApplicationUser UserToInvite { get; set; }
        public string NameOfUserToInvite { get; set; }
        public Instrument Instrument { get; set; }

        public AddMemberViewModel()
        {

        }

        public AddMemberViewModel(Band band, ApplicationUser userToAdd, Instrument instrument)
        {
            Instrument = instrument;
            Band = band;
            UserToInvite = userToAdd;
        }

        public AddMemberViewModel(Guid bandId, Guid userToAddGuid, ApplicationDbContext context)
        {
            Band = context.Bands.Find(bandId);
            UserToInvite = context.Users.Find(userToAddGuid);
        }

        //public static AddMemberViewModel GenerateAddMemberViewModel(Guid bandId, string userName, ApplicationDbContext context)
        //{
        //    AddMemberViewModel newModel = new AddMemberViewModel {Band = context.Bands.Find(bandId)};
        //    ApplicationUser userToInvite = context.Users.FirstOrDefault(x => x.UserName == userName);
        //    if (userToInvite == null)
        //    {
        //        return null;
        //    }
        //    newModel.UserToInvite = userToInvite;
        //    return newModel;
        //}
    }
}