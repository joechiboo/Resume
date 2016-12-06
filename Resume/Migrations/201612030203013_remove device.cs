namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedevice : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Devices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        memberid = c.Int(nullable: false),
                        sessionid = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
    }
}
