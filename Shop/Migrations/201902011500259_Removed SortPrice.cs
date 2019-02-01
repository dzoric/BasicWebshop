namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSortPrice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "SortPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "SortPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
