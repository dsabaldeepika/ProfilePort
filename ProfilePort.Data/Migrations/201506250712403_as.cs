namespace ProfilePort.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _as : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeStreetAddress = c.String(),
                        HomeStreetAddress2 = c.String(),
                        HomeCity = c.String(),
                        HomeState = c.String(),
                        HomeZipCode = c.String(),
                        WorkStreetAddress = c.String(),
                        WorkStreetAddress2 = c.String(),
                        WorkCity = c.String(),
                        WorkState = c.String(),
                        WorkZipCode = c.String(),
                        MainUrl = c.String(),
                        FacebookUrl = c.String(),
                        LinkedinUrl = c.String(),
                        TwitterUrl = c.String(),
                        GoogleplusUrl = c.String(),
                        EmailAddress = c.String(),
                        EmailAddress2 = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumber2 = c.String(),
                        DashboardId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dashboards", t => t.DashboardId, cascadeDelete: true)
                .Index(t => t.DashboardId);
            
            CreateTable(
                "dbo.Dashboards",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DashboardName = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        EducationId = c.Int(nullable: false, identity: true),
                        School = c.String(),
                        DatesAttended = c.DateTime(),
                        FieldofStudy = c.String(),
                        Activities = c.String(),
                        Description = c.String(),
                        DashboardId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.EducationId)
                .ForeignKey("dbo.Dashboards", t => t.DashboardId, cascadeDelete: true)
                .Index(t => t.DashboardId);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        FavoriteId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DashboardId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.FavoriteId)
                .ForeignKey("dbo.Dashboards", t => t.DashboardId, cascadeDelete: true)
                .Index(t => t.DashboardId);
            
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        InterestId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DashboardId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.InterestId)
                .ForeignKey("dbo.Dashboards", t => t.DashboardId, cascadeDelete: true)
                .Index(t => t.DashboardId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        Description = c.String(),
                        YearsExperience = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        DashboardId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Dashboards", t => t.DashboardId, cascadeDelete: true)
                .Index(t => t.DashboardId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        To = c.String(),
                        ToId = c.Int(nullable: false),
                        From = c.String(),
                        FromId = c.Int(nullable: false),
                        Subject = c.String(),
                        Content = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateRead = c.DateTime(),
                        DashboardId = c.String(nullable: false, maxLength: 128),
                        DateUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Dashboards", t => t.DashboardId, cascadeDelete: true)
                .Index(t => t.DashboardId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        NoteContent = c.String(),
                        DashboardId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dashboards", t => t.DashboardId, cascadeDelete: true)
                .Index(t => t.DashboardId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DashboardId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SkillId)
                .ForeignKey("dbo.Dashboards", t => t.DashboardId, cascadeDelete: true)
                .Index(t => t.DashboardId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        Created = c.DateTime(nullable: false),
                        LastLogin = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
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
                "dbo.Profiles",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        Sex = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        SocialSecurity = c.String(),
                        PicFile = c.String(),
                        IsLookingForJob = c.Boolean(nullable: false),
                        DashboardId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ProfileId)
                .ForeignKey("dbo.Dashboards", t => t.DashboardId, cascadeDelete: true)
                .Index(t => t.DashboardId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Profiles", "DashboardId", "dbo.Dashboards");
            DropForeignKey("dbo.ContactInfoes", "DashboardId", "dbo.Dashboards");
            DropForeignKey("dbo.Dashboards", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Skills", "DashboardId", "dbo.Dashboards");
            DropForeignKey("dbo.Notes", "DashboardId", "dbo.Dashboards");
            DropForeignKey("dbo.Messages", "DashboardId", "dbo.Dashboards");
            DropForeignKey("dbo.Jobs", "DashboardId", "dbo.Dashboards");
            DropForeignKey("dbo.Interests", "DashboardId", "dbo.Dashboards");
            DropForeignKey("dbo.Favorites", "DashboardId", "dbo.Dashboards");
            DropForeignKey("dbo.Educations", "DashboardId", "dbo.Dashboards");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Profiles", new[] { "DashboardId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Skills", new[] { "DashboardId" });
            DropIndex("dbo.Notes", new[] { "DashboardId" });
            DropIndex("dbo.Messages", new[] { "DashboardId" });
            DropIndex("dbo.Jobs", new[] { "DashboardId" });
            DropIndex("dbo.Interests", new[] { "DashboardId" });
            DropIndex("dbo.Favorites", new[] { "DashboardId" });
            DropIndex("dbo.Educations", new[] { "DashboardId" });
            DropIndex("dbo.Dashboards", new[] { "User_Id" });
            DropIndex("dbo.ContactInfoes", new[] { "DashboardId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Profiles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Skills");
            DropTable("dbo.Notes");
            DropTable("dbo.Messages");
            DropTable("dbo.Jobs");
            DropTable("dbo.Interests");
            DropTable("dbo.Favorites");
            DropTable("dbo.Educations");
            DropTable("dbo.Dashboards");
            DropTable("dbo.ContactInfoes");
        }
    }
}
