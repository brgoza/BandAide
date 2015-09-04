namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixed_many_to_many_with_owner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bands", "Creator_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Bands", "Creator_Id");
            AddForeignKey("dbo.Bands", "Creator_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bands", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Bands", new[] { "Creator_Id" });
            DropColumn("dbo.Bands", "Creator_Id");
        }
    }
}
