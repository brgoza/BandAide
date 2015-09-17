namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removed_invites_table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invites", "Band_Id", "dbo.Bands");
            DropForeignKey("dbo.Invites", "Invitee_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Invites", "Inviter_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Invites", new[] { "Band_Id" });
            DropIndex("dbo.Invites", new[] { "Invitee_Id" });
            DropIndex("dbo.Invites", new[] { "Inviter_Id" });
            DropTable("dbo.Invites");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Invites",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Active = c.Boolean(nullable: false),
                        Band_Id = c.Guid(),
                        Invitee_Id = c.String(maxLength: 128),
                        Inviter_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Invites", "Inviter_Id");
            CreateIndex("dbo.Invites", "Invitee_Id");
            CreateIndex("dbo.Invites", "Band_Id");
            AddForeignKey("dbo.Invites", "Inviter_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Invites", "Invitee_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Invites", "Band_Id", "dbo.Bands", "Id");
        }
    }
}
