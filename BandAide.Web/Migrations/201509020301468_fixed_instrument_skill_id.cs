namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixed_instrument_skill_id : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.InstrumentSkills");
            AddColumn("dbo.InstrumentSkills", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.InstrumentSkills", "Id");
            DropColumn("dbo.InstrumentSkills", "InstrumentSkillId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InstrumentSkills", "InstrumentSkillId", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.InstrumentSkills");
            DropColumn("dbo.InstrumentSkills", "Id");
            AddPrimaryKey("dbo.InstrumentSkills", "InstrumentSkillId");
        }
    }
}
