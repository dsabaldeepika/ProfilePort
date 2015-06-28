namespace ProfilePort.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedtabled : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MainProfiles", "DashboardId", "dbo.Dashboards");
            DropIndex("dbo.MainProfiles", new[] { "DashboardId" });
            DropTable("dbo.MainProfiles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MainProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sex = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        SocialSecurity = c.String(),
                        PicFile = c.String(),
                        IsLookingForJob = c.Boolean(nullable: false),
                        DashboardId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.MainProfiles", "DashboardId");
            AddForeignKey("dbo.MainProfiles", "DashboardId", "dbo.Dashboards", "Id", cascadeDelete: true);
        }
    }
}
