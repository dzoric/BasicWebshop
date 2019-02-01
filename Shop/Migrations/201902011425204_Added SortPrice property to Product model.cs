namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSortPricepropertytoProductmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SortPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "SortPrice");
        }
    }
}
