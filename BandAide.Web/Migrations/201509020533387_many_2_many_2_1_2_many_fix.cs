namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class many_2_many_2_1_2_many_fix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Instrument_Id", "dbo.Instruments");
            DropIndex("dbo.AspNetUsers", new[] { "Instrument_Id" });
            CreateTable(
                "dbo.ApplicationUserInstruments",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Instrument_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Instrument_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Instruments", t => t.Instrument_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Instrument_Id);
            
            DropColumn("dbo.AspNetUsers", "Instrument_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Instrument_Id", c => c.Guid());
            DropForeignKey("dbo.ApplicationUserInstruments", "Instrument_Id", "dbo.Instruments");
            DropForeignKey("dbo.ApplicationUserInstruments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserInstruments", new[] { "Instrument_Id" });
            DropIndex("dbo.ApplicationUserInstruments", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserInstruments");
            CreateIndex("dbo.AspNetUsers", "Instrument_Id");
            AddForeignKey("dbo.AspNetUsers", "Instrument_Id", "dbo.Instruments", "Id");
        }
    }
}
