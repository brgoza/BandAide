namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_image_props : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ImageArray", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ImageArray");
        }
    }
}
