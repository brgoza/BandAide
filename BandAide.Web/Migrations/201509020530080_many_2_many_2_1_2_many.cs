namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class many_2_many_2_1_2_many : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InstrumentSkillApplicationUsers", "InstrumentSkill_Id", "dbo.InstrumentSkills");
            DropForeignKey("dbo.InstrumentSkillApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.InstrumentSkillApplicationUsers", new[] { "InstrumentSkill_Id" });
            DropIndex("dbo.InstrumentSkillApplicationUsers", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.InstrumentSkills", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.InstrumentSkills", "ApplicationUser_Id");
            AddForeignKey("dbo.InstrumentSkills", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.InstrumentSkillApplicationUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InstrumentSkillApplicationUsers",
                c => new
                    {
                        InstrumentSkill_Id = c.Guid(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.InstrumentSkill_Id, t.ApplicationUser_Id });
            
            DropForeignKey("dbo.InstrumentSkills", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.InstrumentSkills", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.InstrumentSkills", "ApplicationUser_Id");
            CreateIndex("dbo.InstrumentSkillApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.InstrumentSkillApplicationUsers", "InstrumentSkill_Id");
            AddForeignKey("dbo.InstrumentSkillApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InstrumentSkillApplicationUsers", "InstrumentSkill_Id", "dbo.InstrumentSkills", "Id", cascadeDelete: true);
        }
    }
}
