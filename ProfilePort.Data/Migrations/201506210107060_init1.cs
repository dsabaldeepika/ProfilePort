namespace ProfilePort.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Profiles");
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.Profiles", "ProfileId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Profiles", "ProfileId");
            DropColumn("dbo.Profiles", "Id");
            DropColumn("dbo.Profiles", "FirstName");
            DropColumn("dbo.Profiles", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profiles", "LastName", c => c.String());
            AddColumn("dbo.Profiles", "FirstName", c => c.String());
            AddColumn("dbo.Profiles", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Profiles");
            DropColumn("dbo.Profiles", "ProfileId");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            AddPrimaryKey("dbo.Profiles", "Id");
        }
    }
}
