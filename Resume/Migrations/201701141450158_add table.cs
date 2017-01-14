namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Information", "tableid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Information", "tableid");
            DropTable("dbo.Tables");
        }
    }
}
