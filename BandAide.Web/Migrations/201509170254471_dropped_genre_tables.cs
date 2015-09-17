namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropped_genre_tables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GenreBands", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.GenreBands", "Band_Id", "dbo.Bands");
            DropIndex("dbo.GenreBands", new[] { "Genre_Id" });
            DropIndex("dbo.GenreBands", new[] { "Band_Id" });
            AddColumn("dbo.Bands", "Genre_Id", c => c.Guid());
            CreateIndex("dbo.Bands", "Genre_Id");
            AddForeignKey("dbo.Bands", "Genre_Id", "dbo.Genres", "Id");
            DropTable("dbo.GenreBands");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GenreBands",
                c => new
                    {
                        Genre_Id = c.Guid(nullable: false),
                        Band_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Band_Id });
            
            DropForeignKey("dbo.Bands", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Bands", new[] { "Genre_Id" });
            DropColumn("dbo.Bands", "Genre_Id");
            CreateIndex("dbo.GenreBands", "Band_Id");
            CreateIndex("dbo.GenreBands", "Genre_Id");
            AddForeignKey("dbo.GenreBands", "Band_Id", "dbo.Bands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GenreBands", "Genre_Id", "dbo.Genres", "Id", cascadeDelete: true);
        }
    }
}
