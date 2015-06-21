namespace ProfilePort.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class REal : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Educations", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Educations", "Description", c => c.String());
        }
    }
}
