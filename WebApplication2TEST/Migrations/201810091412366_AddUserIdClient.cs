namespace WebApplication2TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdClient : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Clients", new[] { "Email" });
            AddColumn("dbo.Clients", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Clients", "UserId");
            AddForeignKey("dbo.Clients", "UserId", "dbo.Users", "Id");
            DropColumn("dbo.Clients", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Email", c => c.String(nullable: false, maxLength: 60));
            DropForeignKey("dbo.Clients", "UserId", "dbo.Users");
            DropIndex("dbo.Clients", new[] { "UserId" });
            DropColumn("dbo.Clients", "UserId");
            CreateIndex("dbo.Clients", "Email", unique: true);
        }
    }
}
