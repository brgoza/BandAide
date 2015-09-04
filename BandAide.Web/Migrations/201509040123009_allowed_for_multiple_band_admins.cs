namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowed_for_multiple_band_admins : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserBands", newName: "BandMembers");
            DropForeignKey("dbo.Bands", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Bands", new[] { "Creator_Id" });
            RenameColumn(table: "dbo.BandMembers", name: "ApplicationUser_Id", newName: "Bands");
            RenameColumn(table: "dbo.BandMembers", name: "Band_Id", newName: "ApplicationUsers");
            RenameIndex(table: "dbo.BandMembers", name: "IX_ApplicationUser_Id", newName: "IX_Bands");
            RenameIndex(table: "dbo.BandMembers", name: "IX_Band_Id", newName: "IX_ApplicationUsers");
            CreateTable(
                "dbo.BandAdmins",
                c => new
                    {
                        Bands = c.String(nullable: false, maxLength: 128),
                        ApplicationUsers = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Bands, t.ApplicationUsers })
                .ForeignKey("dbo.AspNetUsers", t => t.Bands, cascadeDelete: true)
                .ForeignKey("dbo.Bands", t => t.ApplicationUsers, cascadeDelete: true)
                .Index(t => t.Bands)
                .Index(t => t.ApplicationUsers);
            
            DropColumn("dbo.Bands", "Creator_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bands", "Creator_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.BandAdmins", "ApplicationUsers", "dbo.Bands");
            DropForeignKey("dbo.BandAdmins", "Bands", "dbo.AspNetUsers");
            DropIndex("dbo.BandAdmins", new[] { "ApplicationUsers" });
            DropIndex("dbo.BandAdmins", new[] { "Bands" });
            DropTable("dbo.BandAdmins");
            RenameIndex(table: "dbo.BandMembers", name: "IX_ApplicationUsers", newName: "IX_Band_Id");
            RenameIndex(table: "dbo.BandMembers", name: "IX_Bands", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.BandMembers", name: "ApplicationUsers", newName: "Band_Id");
            RenameColumn(table: "dbo.BandMembers", name: "Bands", newName: "ApplicationUser_Id");
            CreateIndex("dbo.Bands", "Creator_Id");
            AddForeignKey("dbo.Bands", "Creator_Id", "dbo.AspNetUsers", "Id");
            RenameTable(name: "dbo.BandMembers", newName: "ApplicationUserBands");
        }
    }
}
