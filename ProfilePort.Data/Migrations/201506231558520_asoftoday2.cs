using System;
using System.Data.Entity.Migrations;

public partial class asoftoday2 : DbMigration
{
    public override void Up()
    {
      
       
        
    }
    
    public override void Down()
    {
        DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
        DropForeignKey("dbo.Profiles", "UserId", "dbo.AspNetUsers");
        DropForeignKey("dbo.ContactInfos", "UserId", "dbo.AspNetUsers");
        DropForeignKey("dbo.Skills", "UserId", "dbo.AspNetUsers");
        DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
        DropForeignKey("dbo.Notes", "UserId", "dbo.AspNetUsers");
        DropForeignKey("dbo.Messages", "UserId", "dbo.AspNetUsers");
        DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
        DropForeignKey("dbo.Jobs", "UserId", "dbo.AspNetUsers");
        DropForeignKey("dbo.Interests", "UserId", "dbo.AspNetUsers");
        DropForeignKey("dbo.Favorites", "UserId", "dbo.AspNetUsers");
        DropForeignKey("dbo.Educations", "UserId", "dbo.AspNetUsers");
        DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
        DropIndex("dbo.AspNetRoles", "RoleNameIndex");
        DropIndex("dbo.Profiles", new[] { "UserId" });
        DropIndex("dbo.Skills", new[] { "UserId" });
        DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
        DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
        DropIndex("dbo.Notes", new[] { "UserId" });
        DropIndex("dbo.Messages", new[] { "UserId" });
        DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
        DropIndex("dbo.Jobs", new[] { "UserId" });
        DropIndex("dbo.Interests", new[] { "UserId" });
        DropIndex("dbo.Favorites", new[] { "UserId" });
        DropIndex("dbo.Educations", new[] { "UserId" });
        DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
        DropIndex("dbo.AspNetUsers", "UserNameIndex");
        DropIndex("dbo.ContactInfos", new[] { "UserId" });
        DropTable("dbo.AspNetRoles");
        DropTable("dbo.Profiles");
        DropTable("dbo.Skills");
        DropTable("dbo.AspNetUserRoles");
        DropTable("dbo.Notes");
        DropTable("dbo.Messages");
        DropTable("dbo.AspNetUserLogins");
        DropTable("dbo.Jobs");
        DropTable("dbo.Interests");
        DropTable("dbo.Favorites");
        DropTable("dbo.Educations");
        DropTable("dbo.AspNetUserClaims");
        DropTable("dbo.AspNetUsers");
        DropTable("dbo.ContactInfos");
    }
}
