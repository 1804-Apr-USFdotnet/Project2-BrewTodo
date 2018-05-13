namespace BrewTodo.Migrations
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
                        BeerName = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false, maxLength: 200),
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
                        BeerTypeName = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.BeerTypeID);
            
            CreateTable(
                "dbo.Breweries",
                c => new
                    {
                        BreweryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        ImageURL = c.String(),
                        Address = c.String(maxLength: 200),
                        ZipCode = c.Int(nullable: false),
                        StateID = c.Int(nullable: false),
                        PhoneNumber = c.String(maxLength: 20),
                        BusinessHours = c.String(maxLength: 50),
                        HasTShirt = c.Boolean(nullable: false),
                        HasMug = c.Boolean(nullable: false),
                        HasGrowler = c.Boolean(nullable: false),
                        HasFood = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BreweryID)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: true)
                .Index(t => t.StateID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        StateAbbr = c.String(),
                    })
                .PrimaryKey(t => t.StateID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        ReviewDescription = c.String(maxLength: 200),
                        Rating = c.Single(nullable: false),
                        UserID = c.Int(nullable: false),
                        BreweryID = c.Int(nullable: false),
                        User_UsersID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.Breweries", t => t.BreweryID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UsersID)
                .Index(t => t.BreweryID)
                .Index(t => t.User_UsersID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UsersID = c.String(nullable: false, maxLength: 128),
                        Username = c.String(maxLength: 20),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        ProfileImageURL = c.Binary(),
                    })
                .PrimaryKey(t => t.UsersID)
                .ForeignKey("dbo.AspNetUsers", t => t.UsersID)
                .Index(t => t.UsersID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserBeerTrieds",
                c => new
                    {
                        UserBeerTriedID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        BreweryID = c.Int(nullable: false),
                        User_UsersID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserBeerTriedID)
                .ForeignKey("dbo.Breweries", t => t.BreweryID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UsersID)
                .Index(t => t.BreweryID)
                .Index(t => t.User_UsersID);
            
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
                        User_UsersID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserPurchasedItemID)
                .ForeignKey("dbo.Breweries", t => t.BreweryID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UsersID)
                .Index(t => t.BreweryID)
                .Index(t => t.User_UsersID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPurchasedItems", "User_UsersID", "dbo.Users");
            DropForeignKey("dbo.UserPurchasedItems", "BreweryID", "dbo.Breweries");
            DropForeignKey("dbo.UserBeerTrieds", "User_UsersID", "dbo.Users");
            DropForeignKey("dbo.UserBeerTrieds", "BreweryID", "dbo.Breweries");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reviews", "User_UsersID", "dbo.Users");
            DropForeignKey("dbo.Users", "UsersID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "BreweryID", "dbo.Breweries");
            DropForeignKey("dbo.Beers", "BreweryID", "dbo.Breweries");
            DropForeignKey("dbo.Breweries", "StateID", "dbo.States");
            DropForeignKey("dbo.Beers", "BeerTypeID", "dbo.BeerTypes");
            DropIndex("dbo.UserPurchasedItems", new[] { "User_UsersID" });
            DropIndex("dbo.UserPurchasedItems", new[] { "BreweryID" });
            DropIndex("dbo.UserBeerTrieds", new[] { "User_UsersID" });
            DropIndex("dbo.UserBeerTrieds", new[] { "BreweryID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "UsersID" });
            DropIndex("dbo.Reviews", new[] { "User_UsersID" });
            DropIndex("dbo.Reviews", new[] { "BreweryID" });
            DropIndex("dbo.Breweries", new[] { "StateID" });
            DropIndex("dbo.Beers", new[] { "BreweryID" });
            DropIndex("dbo.Beers", new[] { "BeerTypeID" });
            DropTable("dbo.UserPurchasedItems");
            DropTable("dbo.UserBeerTrieds");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.States");
            DropTable("dbo.Breweries");
            DropTable("dbo.BeerTypes");
            DropTable("dbo.Beers");
        }
    }
}
