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

        #region SeedData
        public static List<string> UserData =
            File.ReadAllLines(@"C:\Users\brgoz\Source\Repos\BandAide\SeedData\UserData.csv").ToList();

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
            SeedUsers(context);
            context.SaveChanges();
            SeedInstruments(context);
            context.SaveChanges();
            SeedInstrumentSkills(context);
            context.SaveChanges();
        }


        protected void SeedUsers(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            // WTF Resharper?  foreach (var userToInsert in from line in UserData let email = line.Split(',')[2] where !context.Users.Any(u => u.UserName == email) select line.Split(',') into fields select new ApplicationUser

            foreach (var line in UserData)
            {
                string email = line.Split(',')[2];
                if (context.Users.Any(u => u.UserName == email)) continue;
                var fields = line.Split(',');

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
                    DOB = Utility.Seeder.RandomDOB(13, 100)
                };
                userManager.Create(userToInsert, "Password@123");
            }
        }

        public void SeedInstruments(ApplicationDbContext context)
        {
            foreach (string i in InstrumentNames.Where(i => !context.InstrumentsDbSet.Any(x => x.Name == i)))
            {
                context.InstrumentsDbSet.Add(new Instrument(i));
            }
        }

        public static void SeedInstrumentSkills(ApplicationDbContext context)
        {
            Random rnd = new Random();
            var users = context.Users.ToList();
            foreach (var user in users)
            {
                if (user.InstrumentSkills.Count > 0) continue;
            
                var x = rnd.Next(1, 4);
                for (int i = 0; i < x; i++)
                {
                    var prof = (Proficiency)rnd.Next(1, 6);
                    var newSkill = new InstrumentSkill(Utility.Seeder.RandomInstrument(context), prof, "", user);
                    user.InstrumentSkills.Add(newSkill);
               
                }
                context.SaveChanges();
            }
         
        }

    }
}
