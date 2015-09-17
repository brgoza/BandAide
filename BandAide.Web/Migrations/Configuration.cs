using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using BandAide.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Provider;

namespace BandAide.Web.Migrations
{
    public class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        #region SeedSources

        public static List<string> InstrumentNames = new List<string>
        {
            "Piano",
            "Guitar",
            "Bass",
            "Drums",
            "Vocal"
        };

        #endregion

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (Debugger.IsAttached == false)
                Debugger.Launch();

            SeedUsers(context);
            context.SaveChanges();
            SeedInstruments(context);
            context.SaveChanges();
            SeedInstrumentSkills(context);
            context.SaveChanges();
            SeedBands(context, 10, 2);
            context.SaveChanges();
            SeedRandomLonersLookingForBand(context, 10);
            context.SaveChanges();
        }

        protected void SeedUsers(ApplicationDbContext context)
        {
            var userData =
                 File.ReadAllLines(@"C:\Users\brgoz\Source\Repos\BandAide\BandAide.Web\App_Data\userData.csv").ToList();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            foreach (var userToInsert in (from line in userData
                                          select line.Split(',')
                into fields
                                          let email = fields[2]
                                          where !context.Users.Any(u => u.UserName == email) && fields[5] == "AR"
                                          select new ApplicationUser
                                          {
                                              UserName = fields[2],
                                              Email = fields[2],
                                              FirstName = fields[0],
                                              LastName = fields[1],
                                              StreetAddress = fields[3],
                                              City = fields[4],
                                              State = fields[5],
                                              Zip = fields[6],
                                              PhoneNumber = "0797697898",
                                              DOB = Utility.RandomDOB(13, 100)
                                          }).Where(userToInsert => !context.Users.Any(x => x.UserName == userToInsert.UserName)))
            {
                userManager.Create(userToInsert, "Password@123");
            }
        }
        protected void SeedInstruments(ApplicationDbContext context)
        {
            foreach (var instr in InstrumentNames.Where(i => !context.Instruments.Any(x => x.Name == i)).Select(i => new Instrument { Name = i }))
            {
                context.Instruments.Add(instr);
            }
        }
        protected void SeedInstrumentSkills(ApplicationDbContext context)
        {
            var rnd = new Random();
            var users = context.Users.ToList();
            foreach (var user in users)
            {
                if (user.InstrumentSkills.Count > 0) continue;

                var x = rnd.Next(1, 2);
                for (var i = 0; i < x; i++)
                {
                    var prof = (Proficiency)rnd.Next(1, 6);
                    var newSkill = new InstrumentSkill(Utility.RandomInstrument(context), prof, "", user);
                    user.InstrumentSkills.Add(newSkill);
                }
            }
        }
        protected void SeedBands(ApplicationDbContext context, int maxCount, int maxMembers)
        {
            var bandNames =
                System.IO.File.ReadAllLines(@"C:\Users\brgoz\Source\Repos\BandAide\BandAide.Web\App_Data\bandNames.csv").ToList();

            int bandCount = context.Bands.Count();
            if (bandCount >= maxCount) return;

            while (context.Bands.Count() <= maxCount)
            {
                var randomUser = context.Users.OrderBy(x => Guid.NewGuid()).First();
                if (randomUser.MemberOfBands.Count > 0) continue;
                var randomBandName = bandNames.OrderBy(x => Guid.NewGuid()).First();
                var newBand = Band.SaveNewBandToDB(randomBandName, randomUser, context);
                var randomInstrument = context.Instruments.OrderBy(y => Guid.NewGuid()).First();
                if (randomUser.BestInstrument != randomInstrument)
                {
                    newBand.NeedMemberQueries.Add(new NeedMemberQuery(newBand, randomInstrument));
                }
                bandCount++;
            }

        }

        protected void SeedRandomLonersLookingForBand(ApplicationDbContext context, int maxCount)
        {
            var loners = context.Users.Where(x => x.MemberOfBands.Count == 0 && x.NeedBandQueries.Count == 0);
            List<NeedBandQuery> results = new List<NeedBandQuery>();
            foreach (var loner in loners)
            {
                results.Add(new NeedBandQuery(loner, loner.BestInstrument));
            }
            context.NeedBandQueriesDbSet.AddRange(results);
        }

    }
}