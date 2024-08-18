using CMS_BackEnd.Migrations;
using CMS_BackEnd.Models;
using System.Data.Entity;

public class CustomerContext : DbContext
{
    public CustomerContext() : base("CustomerDB")
    {
        // Set the initializer to use the Configuration class
        Database.SetInitializer(new MigrateDatabaseToLatestVersion<CustomerContext, Configuration>());
    }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
