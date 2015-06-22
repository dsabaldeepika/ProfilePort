namespace ProfilePort.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inits : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Profiles", "SocialSecurity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profiles", "SocialSecurity", c => c.String());
        }
    }
}
    