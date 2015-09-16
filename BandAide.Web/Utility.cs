using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using BandAide.Web.Models;
using Microsoft.Ajax.Utilities;

namespace BandAide.Web
{
    public static class Utility
    {
        public static byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static byte[] GetUrlImageArray(string Url)
        {
            return new WebClient().DownloadData(Url);
        }

        public static void FixInstrumentSkills(this ApplicationUser usr, ApplicationDbContext db)
        {
            usr.InstrumentSkills = usr.InstrumentSkills.DistinctBy(x => x.Instrument).ToList();
        }

        public static void FixInstrumentSkills(ApplicationDbContext db)
        {
            var users = db.Users.Include("InstrumentSkills.Instrument");
            foreach (var user in users)
            {
                user.InstrumentSkills = user.InstrumentSkills.DistinctBy(x => x.Instrument).ToList();
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
            return db.Instruments.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }
    }
}
