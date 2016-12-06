namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "SessionID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "SessionID");
        }
    }
}
