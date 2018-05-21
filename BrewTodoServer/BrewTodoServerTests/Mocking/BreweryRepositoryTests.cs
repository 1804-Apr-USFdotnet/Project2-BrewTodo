using BrewTodoServer;
using BrewTodoServer.Data;
using BrewTodoServer.Models;
using Effort;
using NUnit.Framework;
using System.Configuration;


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
            const int nonExistingId = 5;
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

            // Act
            _repository.Post(brewery);
            _repository.Post(brewery);
            _repository.Post(brewery);

            var result = _repository.Get(nonExistingId);

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


            _context.Breweries.Add(brewery);
            _context.SaveChanges();

            // Act
            var retrievedBrewerey = _repository.Get(3);

            // Assert
            Assert.IsNull(retrievedBrewerey);
        }
        [Test]
        public void DeleteBrewery_ReturnsNull()
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
            int breweryIdNumber = brewery.BreweryID;
            _repository.Delete(brewery.BreweryID);

            //Act
            var result = _repository.Get(breweryIdNumber);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void DeleteBrewery_BreweryNotDeletedReturnsNotNull()
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
            _repository.Post(brewery);
            _repository.Post(brewery);
            int idDeleted = 2;
            int notDeleted = 3;
            _repository.Delete(idDeleted);

            //Act
            var result = _repository.Get(notDeleted);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteBrewery_BreweryNotDeletedReturnsExpectedBrewery()
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
            _repository.Post(brewery);
            _repository.Post(brewery);

            int idDeleted = 2;
            int notDeleted = 3;
            var expected = _repository.Get(notDeleted);
            _repository.Delete(idDeleted);

            //Act
            var actual = _repository.Get(notDeleted);

            //Assert
            Assert.AreEqual(actual,expected);
        }

        [Test]
        public void PutBrewery_UpdateBreweryName_ReturnsNotEqualName()
        {
            // Arrange
            var state = new State
            {
                StateID = 1,
                StateAbbr = "Fl"
            };
            var originalBrewery = new Brewery
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
            var updatedBrewery = new Brewery
            {

                Name = "Test!!!!!!!!!!",
                Description = "whatever",
                ImageURL = "dsfds",
                Address = "123 Main street",
                ZipCode = "45335",
                StateID = state.StateID,
                PhoneNumber = "fdfsfds",
                BusinessHours = "fdsfdsfs",
                HasFood = true,
                HasGrowler = true,
                HasMug = true,
                HasTShirt = true
            };

            _repository.Post(originalBrewery);
            var expected = "Test";
            int brewId = _repository.Get(1).BreweryID;
            _repository.Put(brewId, updatedBrewery);
            
            //Act
            var actual = _repository.Get(brewId).Name;

            //Assert
            Assert.AreNotEqual(actual, expected);
        }


    }
}
