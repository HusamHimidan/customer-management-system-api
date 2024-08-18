namespace CMS_BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerModel : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Customers", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", new[] { "Email" });
        }
    }
}
