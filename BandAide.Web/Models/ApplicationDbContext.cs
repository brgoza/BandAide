using System.Data.Entity;
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
        public DbSet<Band> Bands { get; set; }
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

            modelBuilder.Entity<ApplicationUser>().HasMany(b => b.MemberOfBands).WithMany(m => m.Members).Map(x =>
            {
                x.ToTable("BandMembers");
                x.MapLeftKey("Bands");
                x.MapRightKey("ApplicationUsers");
            }
            );

            modelBuilder.Entity<ApplicationUser>().HasMany(b => b.AdminOfBands).WithMany(m => m.Admins).Map(x =>
            {
                x.ToTable("BandAdmins"); // third table is named Cookbooks
                x.MapLeftKey("Bands");
                x.MapRightKey("ApplicationUsers");
            });
            ;
        }
    }
}