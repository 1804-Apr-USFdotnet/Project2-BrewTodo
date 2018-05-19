namespace BrewTodoServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAccountAuthenticationModel : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.Accounts");
        }
    }
}
