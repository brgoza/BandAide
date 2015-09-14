using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using BandAide.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BandAide.Web.Migrations
{
    public class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

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

        protected override void Seed(ApplicationDbContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            SeedUsers(context);
            context.SaveChanges();
            SeedInstruments(context);
            context.SaveChanges();
            SeedInstrumentSkills(context);
            context.SaveChanges();
        }

        protected void SeedUsers(ApplicationDbContext context)
        {
            var file = @"C:\Users\brgoz\Source\Repos\BandAide\BandAide.Web\App_Data\userData.csv";
            List<string> UserData = File.ReadAllLines(file).ToList();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            foreach (var line in UserData)
            {
                var fields = line.Split(',');
                var email = fields[2];
                if (context.Users.Any(u => u.UserName == email) || fields[5] != "AR") continue;

                var userToInsert = new ApplicationUser
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
                };
                userManager.Create(userToInsert, "Password@123");
            }
        }
        protected void SeedInstruments(ApplicationDbContext context)
        {
            foreach (var i in InstrumentNames.Where(i => !context.InstrumentsDbSet.Any(x => x.Name == i)))
            {
                var instr = new Instrument();
                instr.Name = i;
                context.InstrumentsDbSet.Add(instr);
            }
        }
        protected void SeedInstrumentSkills(ApplicationDbContext context)
        {
            var rnd = new Random();
            var users = context.Users.ToList();
            foreach (var user in users)
            {
                if (user.InstrumentSkills.Count > 0) continue;

                var x = rnd.Next(1, 4);
                for (var i = 0; i < x; i++)
                {
                    var prof = (Proficiency)rnd.Next(1, 6);
                    var newSkill = new InstrumentSkill(Utility.RandomInstrument(context), prof, "", user);
                    user.InstrumentSkills.Add(newSkill);
                }
            }
        }

    }
}