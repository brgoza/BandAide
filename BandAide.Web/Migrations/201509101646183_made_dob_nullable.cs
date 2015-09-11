namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class made_dob_nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "DOB", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DOB", c => c.DateTime(nullable: false));
        }
    }
}
