namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class band_and_applictionusers_linked : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Band_Id", "dbo.Bands");
            DropIndex("dbo.AspNetUsers", new[] { "Band_Id" });
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
            
            DropColumn("dbo.AspNetUsers", "Band_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Band_Id", c => c.Guid());
            DropForeignKey("dbo.ApplicationUserBands", "Band_Id", "dbo.Bands");
            DropForeignKey("dbo.ApplicationUserBands", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserBands", new[] { "Band_Id" });
            DropIndex("dbo.ApplicationUserBands", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserBands");
            CreateIndex("dbo.AspNetUsers", "Band_Id");
            AddForeignKey("dbo.AspNetUsers", "Band_Id", "dbo.Bands", "Id");
        }
    }
}
