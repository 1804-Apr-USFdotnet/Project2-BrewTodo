using BrewTodoServer;
using BrewTodoServer.Data;
using BrewTodoServer.Models;
using Effort;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;


namespace BrewTodoServerTests.Mocking
{
    [TestFixture]
    public class BreweryRepositoryTests
    {
      
        private DbContext _context;
        private IRepository<Brewery> _repository;

        [SetUp]
        public void SetUp()
        {
            string connStr = ConfigurationManager.ConnectionStrings["BrewTodoDb"].ConnectionString;
            var connection = DbConnectionFactory.CreateTransient();
            _context = new DbContext(connection);
            _repository = new BreweryRepository(_context);
        }

        [Test]
        public void GetBrewery_WithNonExistingId_ReturnsNull()
        {
            // Arrange
            const int nonExistingId = 1;

            // Act
            var brewery = _repository.Get(nonExistingId);

            // Assert
            Assert.That(brewery, Is.Null);
        }

        [Test]
        public void GetBrewery_WithExistingId_ReturnsBrewery()
        {
            // Arrange
            var state = new State
            {
                StateID = 1,
                StateAbbr = "Fl"
            };
            var brewery = new Brewery {
                
                Name = "Test",
                Description = "whatever",
                ImageURL = "dsfds",
                Address = "123 Main street",
                ZipCode = "45335",
                State = state,
                PhoneNumber = "fdfsfds",
                BusinessHours = "fdsfdsfs",
                HasFood = true,
                HasGrowler = true,
                HasMug = true,
                HasTShirt = true
            };

            
            _repository.Post(brewery);

            // Act
            var retrievedBrewrey = _repository.Get(brewery.BreweryID);
            Assert.AreEqual(brewery.Name, retrievedBrewrey.Name);
            Assert.AreEqual(brewery.BreweryID, retrievedBrewrey.BreweryID);
        }

        [Test]
        public void GetBrewery_WithNonExistentId_ReturnsNull()
        {
            // Arrange
            var state = new State
            {
                StateID = 1,
                StateAbbr = "Fl"
            };
            var brewery = new Brewery
            {

                Name = "Test",
                Description = "whatever",
                ImageURL = "dsfds",
                Address = "123 Main street",
                ZipCode = "45335",
                State = state,
                PhoneNumber = "fdfsfds",
                BusinessHours = "fdsfdsfs",
                HasFood = true,
                HasGrowler = true,
                HasMug = true,
                HasTShirt = true
            };


            _repository.Post(brewery);

            // Act
            var retrievedBrewerey = _repository.Get(3);

            // Assert
            Assert.IsNull(retrievedBrewerey);
        }


    }
}
