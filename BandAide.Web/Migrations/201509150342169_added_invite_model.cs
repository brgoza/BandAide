namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_invite_model : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bands", t => t.Band_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Invitee_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Inviter_Id)
                .Index(t => t.Band_Id)
                .Index(t => t.Invitee_Id)
                .Index(t => t.Inviter_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invites", "Inviter_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Invites", "Invitee_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Invites", "Band_Id", "dbo.Bands");
            DropIndex("dbo.Invites", new[] { "Inviter_Id" });
            DropIndex("dbo.Invites", new[] { "Invitee_Id" });
            DropIndex("dbo.Invites", new[] { "Band_Id" });
            DropTable("dbo.Invites");
        }
    }
}
