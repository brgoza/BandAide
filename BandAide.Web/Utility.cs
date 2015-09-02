using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BandAide.Web.Models;
using Microsoft.Ajax.Utilities;

namespace BandAide.Web
{
    public static class Utility
    {
        public static class Seeder
        {
            public static void FixInstrumentSkills(ApplicationDbContext db)
            {
                var users = db.Users.Include("InstrumentSkills.Instrument");
                foreach (var user in users)
                {
                    user.InstrumentSkills= user.InstrumentSkills.DistinctBy(x => x.Instrument).ToList();
                }
                db.SaveChanges();
            }
            public static void FilterToArkansas(ApplicationDbContext db)
            {
                var users = db.Users;
                foreach (var user in users)
                {
                    if (user.State != "AR") db.Users.Remove(user);
                }
                db.SaveChanges();
            }

            public static void CleanUserDobs(int minAge, int maxAge, ApplicationDbContext db)
            {
                foreach (var user in db.Users)
                {
                    user.DOB = RandomDOB(minAge, maxAge);
                }
                db.SaveChanges();
            }

            public static int RandomSeed;
            public static DateTime RandomDOB(int minAge, int maxAge)
            {
                RandomSeed++;
                var start = DateTime.Now - TimeSpan.FromDays(365 * maxAge);
                var end = DateTime.Now - TimeSpan.FromDays(365 * minAge);
                var gen = new Random(RandomSeed);

                var range = (end - start).Days;
                return start.AddDays(gen.Next(range));
            }

            public static Instrument RandomInstrument(ApplicationDbContext db)
            {
                return db.InstrumentsDbSet.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            }
        }
    }
}