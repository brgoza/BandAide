namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class many_2_many : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InstrumentSkills", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.InstrumentSkills", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.InstrumentSkillApplicationUsers",
                c => new
                    {
                        InstrumentSkill_Id = c.Guid(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.InstrumentSkill_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.InstrumentSkills", t => t.InstrumentSkill_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.InstrumentSkill_Id)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.InstrumentSkills", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InstrumentSkills", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.InstrumentSkillApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.InstrumentSkillApplicationUsers", "InstrumentSkill_Id", "dbo.InstrumentSkills");
            DropIndex("dbo.InstrumentSkillApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.InstrumentSkillApplicationUsers", new[] { "InstrumentSkill_Id" });
            DropTable("dbo.InstrumentSkillApplicationUsers");
            CreateIndex("dbo.InstrumentSkills", "ApplicationUser_Id");
            AddForeignKey("dbo.InstrumentSkills", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
