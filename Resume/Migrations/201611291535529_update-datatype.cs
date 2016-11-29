namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Devices", "sessionid", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Devices", "sessionid", c => c.Int(nullable: false));
        }
    }
}
