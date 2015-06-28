namespace ProfilePort.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedtable : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dashboards", t => t.DashboardId, cascadeDelete: true)
                .Index(t => t.DashboardId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MainProfiles", "DashboardId", "dbo.Dashboards");
            DropIndex("dbo.MainProfiles", new[] { "DashboardId" });
            DropTable("dbo.MainProfiles");
        }
    }
}
