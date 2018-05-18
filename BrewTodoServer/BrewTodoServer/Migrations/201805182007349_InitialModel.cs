namespace BrewTodoServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beers",
                c => new
                    {
                        BeerID = c.Int(nullable: false, identity: true),
                        BeerName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        BeerTypeID = c.Int(nullable: false),
                        BreweryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BeerID)
                .ForeignKey("dbo.BeerTypes", t => t.BeerTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Breweries", t => t.BreweryID, cascadeDelete: true)
                .Index(t => t.BeerTypeID)
                .Index(t => t.BreweryID);
            
            CreateTable(
                "dbo.BeerTypes",
                c => new
                    {
                        BeerTypeID = c.Int(nullable: false, identity: true),
                        BeerTypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BeerTypeID);
            
            CreateTable(
                "dbo.Breweries",
                c => new
                    {
                        BreweryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        ImageURL = c.String(),
                        Address = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        StateID = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        BusinessHours = c.String(),
                        HasTShirt = c.Boolean(nullable: false),
                        HasMug = c.Boolean(nullable: false),
                        HasGrowler = c.Boolean(nullable: false),
                        HasFood = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BreweryID)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: true)
                .Index(t => t.StateID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        ReviewDescription = c.String(),
                        Rating = c.Single(nullable: false),
                        UserID = c.Int(nullable: false),
                        BreweryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Breweries", t => t.BreweryID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.BreweryID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        IdentityID = c.String(),
                        Username = c.String(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ProfileImage = c.Binary(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        StateAbbr = c.String(),
                    })
                .PrimaryKey(t => t.StateID);
            
            CreateTable(
                "dbo.UserBeerTrieds",
                c => new
                    {
                        UserBeerTriedID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        BreweryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserBeerTriedID)
                .ForeignKey("dbo.Breweries", t => t.BreweryID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.BreweryID);
            
            CreateTable(
                "dbo.UserPurchasedItems",
                c => new
                    {
                        UserPurchasedItemID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        BreweryID = c.Int(nullable: false),
                        PurchasedTShirt = c.Boolean(nullable: false),
                        PurchasedMug = c.Boolean(nullable: false),
                        PurchasedGrowler = c.Boolean(nullable: false),
                        TriedFood = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserPurchasedItemID)
                .ForeignKey("dbo.Breweries", t => t.BreweryID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.BreweryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPurchasedItems", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserPurchasedItems", "BreweryID", "dbo.Breweries");
            DropForeignKey("dbo.UserBeerTrieds", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserBeerTrieds", "BreweryID", "dbo.Breweries");
            DropForeignKey("dbo.Beers", "BreweryID", "dbo.Breweries");
            DropForeignKey("dbo.Breweries", "StateID", "dbo.States");
            DropForeignKey("dbo.Reviews", "UserID", "dbo.Users");
            DropForeignKey("dbo.Reviews", "BreweryID", "dbo.Breweries");
            DropForeignKey("dbo.Beers", "BeerTypeID", "dbo.BeerTypes");
            DropIndex("dbo.UserPurchasedItems", new[] { "BreweryID" });
            DropIndex("dbo.UserPurchasedItems", new[] { "UserID" });
            DropIndex("dbo.UserBeerTrieds", new[] { "BreweryID" });
            DropIndex("dbo.UserBeerTrieds", new[] { "UserID" });
            DropIndex("dbo.Reviews", new[] { "BreweryID" });
            DropIndex("dbo.Reviews", new[] { "UserID" });
            DropIndex("dbo.Breweries", new[] { "StateID" });
            DropIndex("dbo.Beers", new[] { "BreweryID" });
            DropIndex("dbo.Beers", new[] { "BeerTypeID" });
            DropTable("dbo.UserPurchasedItems");
            DropTable("dbo.UserBeerTrieds");
            DropTable("dbo.States");
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Breweries");
            DropTable("dbo.BeerTypes");
            DropTable("dbo.Beers");
        }
    }
}
