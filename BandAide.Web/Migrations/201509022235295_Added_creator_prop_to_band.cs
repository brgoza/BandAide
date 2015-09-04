namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_creator_prop_to_band : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserBands", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserBands", "Band_Id", "dbo.Bands");
            DropIndex("dbo.ApplicationUserBands", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserBands", new[] { "Band_Id" });
            AddColumn("dbo.Bands", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Bands", "Creator_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Band_Id", c => c.Guid());
            CreateIndex("dbo.Bands", "ApplicationUser_Id");
            CreateIndex("dbo.Bands", "Creator_Id");
            CreateIndex("dbo.AspNetUsers", "Band_Id");
            AddForeignKey("dbo.Bands", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Bands", "Creator_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Band_Id", "dbo.Bands", "Id");
            DropTable("dbo.ApplicationUserBands");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserBands",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Band_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Band_Id });
            
            DropForeignKey("dbo.AspNetUsers", "Band_Id", "dbo.Bands");
            DropForeignKey("dbo.Bands", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bands", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Band_Id" });
            DropIndex("dbo.Bands", new[] { "Creator_Id" });
            DropIndex("dbo.Bands", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "Band_Id");
            DropColumn("dbo.Bands", "Creator_Id");
            DropColumn("dbo.Bands", "ApplicationUser_Id");
            CreateIndex("dbo.ApplicationUserBands", "Band_Id");
            CreateIndex("dbo.ApplicationUserBands", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserBands", "Band_Id", "dbo.Bands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserBands", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
