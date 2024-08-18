namespace CMS_BackEnd.Migrations
{
    using CMS_BackEnd.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<CustomerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CustomerContext context)
        {
            context.Customers.AddOrUpdate(
           c => c.Email, // Assuming Email is unique, it will prevent duplicate seed data
           new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "123-456-7890", Address = "123 Main St" },
           new Customer { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Phone = "234-567-8901", Address = "456 Elm St" },
           new Customer { Id = 3, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com", Phone = "345-678-9012", Address = "789 Oak St" },
           new Customer { Id = 4, FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com", Phone = "456-789-0123", Address = "321 Pine St" },
           new Customer { Id = 5, FirstName = "Charlie", LastName = "Davis", Email = "charlie.davis@example.com", Phone = "567-890-1234", Address = "654 Cedar St" },
           new Customer { Id = 6, FirstName = "David", LastName = "Wilson", Email = "david.wilson@example.com", Phone = "678-901-2345", Address = "987 Birch St" },
           new Customer { Id = 7, FirstName = "Eva", LastName = "Miller", Email = "eva.miller@example.com", Phone = "789-012-3456", Address = "123 Maple St" },
           new Customer { Id = 8, FirstName = "Frank", LastName = "Clark", Email = "frank.clark@example.com", Phone = "890-123-4567", Address = "456 Walnut St" },
           new Customer { Id = 9, FirstName = "Grace", LastName = "Lewis", Email = "grace.lewis@example.com", Phone = "901-234-5678", Address = "789 Spruce St" },
           new Customer { Id = 10, FirstName = "Henry", LastName = "Walker", Email = "henry.walker@example.com", Phone = "012-345-6789", Address = "321 Ash St" }
       );
        }
    }
}
