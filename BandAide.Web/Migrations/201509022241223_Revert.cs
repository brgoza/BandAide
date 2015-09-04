namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Revert : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bands", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.InstrumentApplicationUsers", "Instrument_Id", "dbo.Instruments");
            DropForeignKey("dbo.InstrumentApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bands", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Band_Id", "dbo.Bands");
            DropIndex("dbo.Bands", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Bands", new[] { "Creator_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Band_Id" });
            DropIndex("dbo.InstrumentApplicationUsers", new[] { "Instrument_Id" });
            DropIndex("dbo.InstrumentApplicationUsers", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.ApplicationUserBands",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Band_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Band_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Bands", t => t.Band_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Band_Id);
            
            AddColumn("dbo.Instruments", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Instruments", "ApplicationUser_Id");
            AddForeignKey("dbo.Instruments", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Bands", "ApplicationUser_Id");
            DropColumn("dbo.Bands", "Creator_Id");
            DropColumn("dbo.AspNetUsers", "Band_Id");
            DropTable("dbo.InstrumentApplicationUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InstrumentApplicationUsers",
                c => new
                    {
                        Instrument_Id = c.Guid(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Instrument_Id, t.ApplicationUser_Id });
            
            AddColumn("dbo.AspNetUsers", "Band_Id", c => c.Guid());
            AddColumn("dbo.Bands", "Creator_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Bands", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Instruments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserBands", "Band_Id", "dbo.Bands");
            DropForeignKey("dbo.ApplicationUserBands", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserBands", new[] { "Band_Id" });
            DropIndex("dbo.ApplicationUserBands", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Instruments", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Instruments", "ApplicationUser_Id");
            DropTable("dbo.ApplicationUserBands");
            CreateIndex("dbo.InstrumentApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.InstrumentApplicationUsers", "Instrument_Id");
            CreateIndex("dbo.AspNetUsers", "Band_Id");
            CreateIndex("dbo.Bands", "Creator_Id");
            CreateIndex("dbo.Bands", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Band_Id", "dbo.Bands", "Id");
            AddForeignKey("dbo.Bands", "Creator_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.InstrumentApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InstrumentApplicationUsers", "Instrument_Id", "dbo.Instruments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bands", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
