namespace ProfilePort.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "SocialSecurity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "SocialSecurity");
        }
    }
}
