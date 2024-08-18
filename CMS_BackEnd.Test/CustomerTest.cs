using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using CMS_BackEnd.Controllers;
using CMS_BackEnd.Models;
using System.Net;

namespace CMS_BackEnd.Tests
{
    [TestClass]
    public class CustomerTest
    {
        private Mock<CustomerContext> _mockContext;
        private Mock<DbSet<Customer>> _mockSet;
        private CustomersController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Create a mock DbSet for Customers
            _mockSet = new Mock<DbSet<Customer>>();

            // Sample customer data
            var data = new List<Customer>
    {
        new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "1234567890", Address = "123 Street" },
        new Customer { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", Phone = "0987654321", Address = "456 Avenue" }
    }.AsQueryable();

            // Configure the mock DbSet
            _mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Configure FindAsync to return a customer based on ID
            _mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>())) // Use It.IsAny<object[]> to match the FindAsync signature
                .ReturnsAsync((object[] keyValues) => data.FirstOrDefault(c => c.Id == (int)keyValues[0]));

            // Configure Remove to handle customer removal
            _mockSet.Setup(m => m.Remove(It.IsAny<Customer>())).Returns((Customer customer) => customer);

            // Mock the Customers property in the CustomerContext
            _mockContext = new Mock<CustomerContext>();
            _mockContext.Setup(c => c.Customers).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.SaveChangesAsync()).ReturnsAsync(1);

            // Create the controller with the mock context
            _controller = new CustomersController(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetCustomer_ReturnsOk_WhenCustomerExists()
        {
            // Act
            IHttpActionResult actionResult = await _controller.GetCustomer(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Customer>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod]
        public async Task GetCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Act
            IHttpActionResult actionResult = await _controller.GetCustomer(9999);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PostCustomer_ReturnsCreated_WhenCustomerIsValid()
        {
            // Arrange
            var newCustomer = new Customer { Id = 3, FirstName = "Alice", LastName = "Smith", Email = "alice.smith@example.com", Phone = "1234567890", Address = "789 Road" };

            // Act
            IHttpActionResult actionResult = await _controller.PostCustomer(newCustomer);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Customer>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.IsNotNull(createdResult.Content);
            Assert.AreEqual(newCustomer.Id, createdResult.Content.Id);
        }

        [TestMethod]
        public async Task PutCustomer_ReturnsNoContent_WhenCustomerIsValid()
        {
            // Arrange
            var updatedCustomer = new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "1234567890", Address = "123 Updated Street" };

            // Act
            IHttpActionResult actionResult = await _controller.PutCustomer(1, updatedCustomer);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(StatusCodeResult));
            Assert.AreEqual(HttpStatusCode.NoContent, (actionResult as StatusCodeResult).StatusCode);
        }

        [TestMethod]
        public async Task DeleteCustomer_ReturnsOk_WhenCustomerExists()
        {
            // Act
            IHttpActionResult actionResult = await _controller.DeleteCustomer(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Customer>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod]
        public async Task DeleteCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Act
            IHttpActionResult actionResult = await _controller.DeleteCustomer(9999);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
