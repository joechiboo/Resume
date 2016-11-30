namespace Resume.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        Message = c.String(),
                        LogTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        memberid = c.Int(nullable: false),
                        sessionid = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Information",
                c => new
                    {
                        hash = c.Guid(nullable: false),
                        memberid = c.Int(nullable: false),
                        accept = c.Boolean(nullable: false),
                        side = c.Boolean(nullable: false),
                        number = c.Int(nullable: false),
                        children = c.Int(nullable: false),
                        vegetable = c.Int(nullable: false),
                        address = c.String(),
                        bus = c.Boolean(nullable: false),
                        HSR = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.hash);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Hash = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Members");
            DropTable("dbo.Information");
            DropTable("dbo.Devices");
            DropTable("dbo.ChatLogs");
        }
    }
}
