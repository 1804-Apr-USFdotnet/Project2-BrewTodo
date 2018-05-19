namespace BrewTodoServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAuthDbContext : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Accounts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.AccountID);
            
        }
    }
}
