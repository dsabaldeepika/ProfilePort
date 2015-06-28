namespace ProfilePort.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Layouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LayoutName = c.String(),
                        HeaderColor = c.String(),
                        BodyColor = c.String(),
                        FooterColor = c.String(),
                        NavigationBarColor = c.String(),
                        BackgroundColor = c.String(),
                        DashboardId = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dashboards", t => t.DashboardId, cascadeDelete: true)
                .Index(t => t.DashboardId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Layouts", "DashboardId", "dbo.Dashboards");
            DropIndex("dbo.Layouts", new[] { "DashboardId" });
            DropTable("dbo.Layouts");
        }
    }
}
