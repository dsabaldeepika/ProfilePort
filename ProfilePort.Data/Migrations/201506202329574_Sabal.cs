namespace ProfilePort.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sabal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Sex = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        PicFile = c.String(),
                        IsLookingForJob = c.Boolean(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AlterColumn("dbo.Educations", "DatesAttended", c => c.DateTime());
            AlterColumn("dbo.Jobs", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Profiles", new[] { "UserId" });
            AlterColumn("dbo.Jobs", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Educations", "DatesAttended", c => c.DateTime(nullable: false));
            DropTable("dbo.Profiles");
        }
    }
}
