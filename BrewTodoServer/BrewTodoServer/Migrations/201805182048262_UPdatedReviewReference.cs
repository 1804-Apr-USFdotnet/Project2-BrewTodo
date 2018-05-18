namespace BrewTodoServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdatedReviewReference : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "BreweryID", "dbo.Breweries");
            DropIndex("dbo.Reviews", new[] { "BreweryID" });
            RenameColumn(table: "dbo.Reviews", name: "BreweryID", newName: "Brewery_BreweryID");
            AlterColumn("dbo.Reviews", "Brewery_BreweryID", c => c.Int());
            CreateIndex("dbo.Reviews", "Brewery_BreweryID");
            AddForeignKey("dbo.Reviews", "Brewery_BreweryID", "dbo.Breweries", "BreweryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Brewery_BreweryID", "dbo.Breweries");
            DropIndex("dbo.Reviews", new[] { "Brewery_BreweryID" });
            AlterColumn("dbo.Reviews", "Brewery_BreweryID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Reviews", name: "Brewery_BreweryID", newName: "BreweryID");
            CreateIndex("dbo.Reviews", "BreweryID");
            AddForeignKey("dbo.Reviews", "BreweryID", "dbo.Breweries", "BreweryID", cascadeDelete: true);
        }
    }
}
