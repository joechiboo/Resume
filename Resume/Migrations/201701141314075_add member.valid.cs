namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmembervalid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Valid", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Valid");
        }
    }
}
