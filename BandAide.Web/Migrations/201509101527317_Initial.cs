namespace BandAide.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bands",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ImageArray = c.Binary(),
                        DOB = c.DateTime(nullable: false),
                        Bio = c.String(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Instruments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        NeedMemberQuery_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.NeedMemberQueries", t => t.NeedMemberQuery_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.NeedMemberQuery_Id);
            
            CreateTable(
                "dbo.InstrumentSkills",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Proficiency = c.Int(nullable: false),
                        BackgroundDescription = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Instrument_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Instruments", t => t.Instrument_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Instrument_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.NeedBandQueries",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
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
                "dbo.Genres",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.NeedMemberQueries",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.BandAdmins",
                c => new
                    {
                        Bands = c.String(nullable: false, maxLength: 128),
                        ApplicationUsers = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Bands, t.ApplicationUsers })
                .ForeignKey("dbo.AspNetUsers", t => t.Bands, cascadeDelete: true)
                .ForeignKey("dbo.Bands", t => t.ApplicationUsers, cascadeDelete: true)
                .Index(t => t.Bands)
                .Index(t => t.ApplicationUsers);
            
            CreateTable(
                "dbo.BandMembers",
                c => new
                    {
                        Bands = c.String(nullable: false, maxLength: 128),
                        ApplicationUsers = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Bands, t.ApplicationUsers })
                .ForeignKey("dbo.AspNetUsers", t => t.Bands, cascadeDelete: true)
                .ForeignKey("dbo.Bands", t => t.ApplicationUsers, cascadeDelete: true)
                .Index(t => t.Bands)
                .Index(t => t.ApplicationUsers);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Genres", "NeedMemberQuery_Id", "dbo.NeedMemberQueries");
            DropForeignKey("dbo.Instruments", "NeedMemberQuery_Id", "dbo.NeedMemberQueries");
            DropForeignKey("dbo.NeedMemberQueries", "Band_Id", "dbo.Bands");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.NeedBandQueries", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Genres", "NeedBandQuery_Id", "dbo.NeedBandQueries");
            DropForeignKey("dbo.GenreBands", "Band_Id", "dbo.Bands");
            DropForeignKey("dbo.GenreBands", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.BandMembers", "ApplicationUsers", "dbo.Bands");
            DropForeignKey("dbo.BandMembers", "Bands", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.InstrumentSkills", "Instrument_Id", "dbo.Instruments");
            DropForeignKey("dbo.InstrumentSkills", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Instruments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BandAdmins", "ApplicationUsers", "dbo.Bands");
            DropForeignKey("dbo.BandAdmins", "Bands", "dbo.AspNetUsers");
            DropIndex("dbo.GenreBands", new[] { "Band_Id" });
            DropIndex("dbo.GenreBands", new[] { "Genre_Id" });
            DropIndex("dbo.BandMembers", new[] { "ApplicationUsers" });
            DropIndex("dbo.BandMembers", new[] { "Bands" });
            DropIndex("dbo.BandAdmins", new[] { "ApplicationUsers" });
            DropIndex("dbo.BandAdmins", new[] { "Bands" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.NeedMemberQueries", new[] { "Band_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Genres", new[] { "NeedMemberQuery_Id" });
            DropIndex("dbo.Genres", new[] { "NeedBandQuery_Id" });
            DropIndex("dbo.NeedBandQueries", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.InstrumentSkills", new[] { "Instrument_Id" });
            DropIndex("dbo.InstrumentSkills", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Instruments", new[] { "NeedMemberQuery_Id" });
            DropIndex("dbo.Instruments", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.GenreBands");
            DropTable("dbo.BandMembers");
            DropTable("dbo.BandAdmins");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.NeedMemberQueries");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Genres");
            DropTable("dbo.NeedBandQueries");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.InstrumentSkills");
            DropTable("dbo.Instruments");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Bands");
        }
    }
}
