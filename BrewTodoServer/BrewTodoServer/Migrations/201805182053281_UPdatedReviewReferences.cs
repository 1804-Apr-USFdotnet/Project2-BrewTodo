namespace BrewTodoServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UPdatedReviewReferences : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "Brewery_BreweryID", "dbo.Breweries");
            DropIndex("dbo.Reviews", new[] { "Brewery_BreweryID" });
            RenameColumn(table: "dbo.Reviews", name: "Brewery_BreweryID", newName: "BreweryID");
            AlterColumn("dbo.Reviews", "BreweryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "BreweryID");
            AddForeignKey("dbo.Reviews", "BreweryID", "dbo.Breweries", "BreweryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "BreweryID", "dbo.Breweries");
            DropIndex("dbo.Reviews", new[] { "BreweryID" });
            AlterColumn("dbo.Reviews", "BreweryID", c => c.Int());
            RenameColumn(table: "dbo.Reviews", name: "BreweryID", newName: "Brewery_BreweryID");
            CreateIndex("dbo.Reviews", "Brewery_BreweryID");
            AddForeignKey("dbo.Reviews", "Brewery_BreweryID", "dbo.Breweries", "BreweryID");
        }
    }
}
