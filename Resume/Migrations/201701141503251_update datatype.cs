namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tables", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tables", "Name", c => c.Int(nullable: false));
        }
    }
}
