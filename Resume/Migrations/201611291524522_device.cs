namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class device : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        memberid = c.Int(nullable: false),
                        sessionid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Devices");
        }
    }
}
