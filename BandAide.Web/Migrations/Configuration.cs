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
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
      
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //Seeding functionality
        public List<string> UserData =
          File.ReadAllLines(@"C:\Users\brgoz\Source\Repos\BandAide\BandAide.Web\SeedUserData.csv").ToList();

        protected override void Seed(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            foreach (var line in UserData)
            {
                var fields = line.Split(',');
                var email = fields[2];

                if (context.Users.Any(u => u.UserName == email)) continue;

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
                    DOB = RandomDOB(13, 100)
                };
                userManager.Create(userToInsert, "Password@123");
            }
        }

        public DateTime RandomDOB(int minAge, int maxAge)
        {
            var start = DateTime.Now - TimeSpan.FromDays(365*maxAge);
            var end = DateTime.Now - TimeSpan.FromDays(365*minAge);
            var gen = new Random();

            var range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}