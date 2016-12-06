namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsomeattributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Information", "write", c => c.String());
            AddColumn("dbo.Information", "read", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Information", "read");
            DropColumn("dbo.Information", "write");
        }
    }
}
