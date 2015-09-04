namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBand_NeedMemberQuery_NeedBandQuery : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserInstruments", newName: "InstrumentApplicationUsers");
            DropPrimaryKey("dbo.InstrumentApplicationUsers");
            CreateTable(
                "dbo.Bands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        NeedBandQuery_Id = c.Guid(),
                        NeedMemberQuery_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NeedBandQueries", t => t.NeedBandQuery_Id)
                .ForeignKey("dbo.NeedMemberQueries", t => t.NeedMemberQuery_Id)
                .Index(t => t.NeedBandQuery_Id)
                .Index(t => t.NeedMemberQuery_Id);
            
            CreateTable(
                "dbo.NeedBandQueries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Active = c.Boolean(nullable: false),
                        QueryStartedOn = c.DateTime(nullable: false),
                        HitCount = c.Int(nullable: false),
                        QueryText = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.NeedMemberQueries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Active = c.Boolean(nullable: false),
                        QueryStartedOn = c.DateTime(nullable: false),
                        HitCount = c.Int(nullable: false),
                        QueryText = c.String(),
                        Band_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bands", t => t.Band_Id)
                .Index(t => t.Band_Id);
            
            CreateTable(
                "dbo.GenreBands",
                c => new
                    {
                        Genre_Id = c.Guid(nullable: false),
                        Band_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Band_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Bands", t => t.Band_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Band_Id);
            
            AddColumn("dbo.Instruments", "NeedMemberQuery_Id", c => c.Guid());
            AddColumn("dbo.AspNetUsers", "Band_Id", c => c.Guid());
            AddPrimaryKey("dbo.InstrumentApplicationUsers", new[] { "Instrument_Id", "ApplicationUser_Id" });
            CreateIndex("dbo.AspNetUsers", "Band_Id");
            CreateIndex("dbo.Instruments", "NeedMemberQuery_Id");
            AddForeignKey("dbo.AspNetUsers", "Band_Id", "dbo.Bands", "Id");
            AddForeignKey("dbo.Instruments", "NeedMemberQuery_Id", "dbo.NeedMemberQueries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Genres", "NeedMemberQuery_Id", "dbo.NeedMemberQueries");
            DropForeignKey("dbo.Instruments", "NeedMemberQuery_Id", "dbo.NeedMemberQueries");
            DropForeignKey("dbo.NeedMemberQueries", "Band_Id", "dbo.Bands");
            DropForeignKey("dbo.NeedBandQueries", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Genres", "NeedBandQuery_Id", "dbo.NeedBandQueries");
            DropForeignKey("dbo.AspNetUsers", "Band_Id", "dbo.Bands");
            DropForeignKey("dbo.GenreBands", "Band_Id", "dbo.Bands");
            DropForeignKey("dbo.GenreBands", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.GenreBands", new[] { "Band_Id" });
            DropIndex("dbo.GenreBands", new[] { "Genre_Id" });
            DropIndex("dbo.NeedMemberQueries", new[] { "Band_Id" });
            DropIndex("dbo.NeedBandQueries", new[] { "User_Id" });
            DropIndex("dbo.Instruments", new[] { "NeedMemberQuery_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Band_Id" });
            DropIndex("dbo.Genres", new[] { "NeedMemberQuery_Id" });
            DropIndex("dbo.Genres", new[] { "NeedBandQuery_Id" });
            DropPrimaryKey("dbo.InstrumentApplicationUsers");
            DropColumn("dbo.AspNetUsers", "Band_Id");
            DropColumn("dbo.Instruments", "NeedMemberQuery_Id");
            DropTable("dbo.GenreBands");
            DropTable("dbo.NeedMemberQueries");
            DropTable("dbo.NeedBandQueries");
            DropTable("dbo.Genres");
            DropTable("dbo.Bands");
            AddPrimaryKey("dbo.InstrumentApplicationUsers", new[] { "ApplicationUser_Id", "Instrument_Id" });
            RenameTable(name: "dbo.InstrumentApplicationUsers", newName: "ApplicationUserInstruments");
        }
    }
}
