namespace ProfilePort.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactinfoupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactInfoes", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ContactInfoes", "UserId");
            AddForeignKey("dbo.ContactInfoes", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactInfoes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ContactInfoes", new[] { "UserId" });
            DropColumn("dbo.ContactInfoes", "UserId");
        }
    }
}
