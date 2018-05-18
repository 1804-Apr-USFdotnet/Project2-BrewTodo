namespace BrewTodoServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddABVToBeerModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beers", "ABV", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Beers", "ABV");
        }
    }
}
