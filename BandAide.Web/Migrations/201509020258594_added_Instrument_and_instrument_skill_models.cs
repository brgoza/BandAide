namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_Instrument_and_instrument_skill_models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Instruments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InstrumentSkills",
                c => new
                    {
                        InstrumentSkillId = c.Guid(nullable: false),
                        Proficiency = c.Int(nullable: false),
                        BackgroundDescription = c.String(),
                        Instrument_Id = c.Guid(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.InstrumentSkillId)
                .ForeignKey("dbo.Instruments", t => t.Instrument_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Instrument_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "Instrument_Id", c => c.Guid());
            CreateIndex("dbo.AspNetUsers", "Instrument_Id");
            AddForeignKey("dbo.AspNetUsers", "Instrument_Id", "dbo.Instruments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Instrument_Id", "dbo.Instruments");
            DropForeignKey("dbo.InstrumentSkills", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.InstrumentSkills", "Instrument_Id", "dbo.Instruments");
            DropIndex("dbo.InstrumentSkills", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.InstrumentSkills", new[] { "Instrument_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Instrument_Id" });
            DropColumn("dbo.AspNetUsers", "Instrument_Id");
            DropTable("dbo.InstrumentSkills");
            DropTable("dbo.Instruments");
        }
    }
}
