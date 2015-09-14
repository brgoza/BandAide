namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modified_needMemberQueryModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Instruments", "NeedMemberQuery_Id", "dbo.NeedMemberQueries");
            DropIndex("dbo.Instruments", new[] { "NeedMemberQuery_Id" });
            AddColumn("dbo.NeedMemberQueries", "Instrument_Id", c => c.Guid());
            CreateIndex("dbo.NeedMemberQueries", "Instrument_Id");
            AddForeignKey("dbo.NeedMemberQueries", "Instrument_Id", "dbo.Instruments", "Id");
            DropColumn("dbo.Instruments", "NeedMemberQuery_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Instruments", "NeedMemberQuery_Id", c => c.Guid());
            DropForeignKey("dbo.NeedMemberQueries", "Instrument_Id", "dbo.Instruments");
            DropIndex("dbo.NeedMemberQueries", new[] { "Instrument_Id" });
            DropColumn("dbo.NeedMemberQueries", "Instrument_Id");
            CreateIndex("dbo.Instruments", "NeedMemberQuery_Id");
            AddForeignKey("dbo.Instruments", "NeedMemberQuery_Id", "dbo.NeedMemberQueries", "Id");
        }
    }
}
