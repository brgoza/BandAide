namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modified_needBandQueryModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NeedBandQueries", "Instrument_Id", c => c.Guid());
            CreateIndex("dbo.NeedBandQueries", "Instrument_Id");
            AddForeignKey("dbo.NeedBandQueries", "Instrument_Id", "dbo.Instruments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NeedBandQueries", "Instrument_Id", "dbo.Instruments");
            DropIndex("dbo.NeedBandQueries", new[] { "Instrument_Id" });
            DropColumn("dbo.NeedBandQueries", "Instrument_Id");
        }
    }
}
