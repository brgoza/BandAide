using System.Data.Entity;
using System.Web.UI;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BandAide.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }

        public DbSet<InstrumentSkill> InstrumentSkillDbSet { get; set; }
        public DbSet<Instrument> InstrumentsDbSet { get; set; }
        public DbSet<Band> BandsDbSet { get; set; }
        public DbSet<NeedBandQuery> NeedBandQueriesDbSet { get; set; }
        public DbSet<NeedMemberQuery> NeedMemberQueriesDbSet { get; set; }
        public DbSet<Genre> GenresDbSet { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.MemberOfBands).WithMany(b => b.Members);

        }
    }
}