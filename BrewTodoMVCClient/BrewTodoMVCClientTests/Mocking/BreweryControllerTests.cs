using BrewTodoMVCClient;
using BrewTodoMVCClient.Controllers;
using BrewTodoMVCClient.Models;
using Effort;
using NUnit.Framework;
using System.Configuration;
using System.Linq;

namespace BrewTodoMVCClientTests.Mocking
{
    [TestFixture]
    public class BreweryControllerTests
    {
        //private DbContext _context;
        //private IRepository<Brewery> _repository;
        private BreweryController controller;

        [SetUp]
        public void SetUp()
        {
            string connStr = ConfigurationManager.ConnectionStrings["BrewTodoDb"].ConnectionString;
            var connection = DbConnectionFactory.CreateTransient();
            //_context = new DbContext(connection);
            //_repository = new BreweryRepository(_context);
            controller = new BreweryController();
        }

        [Test]
        public void Breweries_Test()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
    }
}
