namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_applicationUser_owner_to_InstrumentSkill : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.InstrumentSkills", name: "ApplicationUser_Id", newName: "Owner_Id");
            RenameIndex(table: "dbo.InstrumentSkills", name: "IX_ApplicationUser_Id", newName: "IX_Owner_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.InstrumentSkills", name: "IX_Owner_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.InstrumentSkills", name: "Owner_Id", newName: "ApplicationUser_Id");
        }
    }
}
