namespace WebApplication2TEST.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrixPersonnedouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Voyages", "PrixParPersonne", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Voyages", "PrixParPersonne", c => c.Decimal(nullable: false, storeType: "money"));
        }
    }
}
