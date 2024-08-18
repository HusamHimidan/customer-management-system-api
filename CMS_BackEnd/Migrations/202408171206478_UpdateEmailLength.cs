namespace CMS_BackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmailLength : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Customers", new[] { "Email" });
            AlterColumn("dbo.Customers", "Email", c => c.String(maxLength: 255));
            CreateIndex("dbo.Customers", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", new[] { "Email" });
            AlterColumn("dbo.Customers", "Email", c => c.String());
            CreateIndex("dbo.Customers", "Email", unique: true);
        }
    }
}
