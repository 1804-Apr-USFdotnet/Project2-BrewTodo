using BrewTodoServer;
using BrewTodoServer.Data;
using BrewTodoServer.Models;
using Effort;
using NUnit.Framework;
using System.Configuration;
using System.Linq;

namespace BrewTodoServerTests.Mocking
{
    [TestFixture]
    public class BeerTypeRepositoryTests
    { 
        private DbContext _context;
        private IRepository<BeerType> _beerTypeRepository;

        [SetUp]
        public void SetUp()
        {
            string connStr = ConfigurationManager.ConnectionStrings["BrewTodoDb"].ConnectionString;
            var connection = DbConnectionFactory.CreateTransient();
            _context = new DbContext(connection);
            _beerTypeRepository = new BeerTypeRepository(_context);
        }

        [Test]
        public void GetBeerType_WithNonExistingId_ReturnsNull()
        {
            // Arrange
            const int nonExistingId = 5;
            // Arrange
            var beerType = GetTestBeerType();
            _context.BeerTypes.Add(beerType);
            _context.SaveChanges();

            // Act
            var result = _beerTypeRepository.Get(nonExistingId);  

            // Assert
            Assert.That(result, Is.Null);
        }
        [Test]
        public void GetBeerType_WithExistingId_ReturnsBeerType()
        {
            // Arrange
            var beerType = GetTestBeerType();
            _context.BeerTypes.Add(beerType);
            _context.SaveChanges();
            int id = _context.BeerTypes.ToList().LastOrDefault().BeerTypeID;

            // Act
            var retrievedType = _beerTypeRepository.Get(id);

            // Assert
            Assert.AreEqual(beerType.BeerTypeName, retrievedType.BeerTypeName);
        }

        [Test]
        public void DeleteBeerType_ReturnsNull()
        {
            // Arrange
            var beerType = GetTestBeerType();
            _context.BeerTypes.Add(beerType);
            _context.SaveChanges();
            int id = _context.BeerTypes.FirstOrDefault().BeerTypeID;

            //Act
            _beerTypeRepository.Delete(id);
            var result = _beerTypeRepository.Get(id);

            //Assert
            Assert.IsNull(result);
        }
        [Test]
        public void DeleteBeerType_BeerTypeNotDeletedReturnsNotNull()
        {
            // Arrange
            var beerType = GetTestBeerType();
            _context.BeerTypes.Add(beerType);
            _context.SaveChanges();
            _context.BeerTypes.Add(beerType);
            _context.SaveChanges();
            _context.BeerTypes.Add(beerType);
            _context.SaveChanges();
            int idDeleted = 2;
            int notDeleted = 3;
            _beerTypeRepository.Delete(idDeleted);

            //Act
            var result = _beerTypeRepository.Get(notDeleted);
            
            //Assert
            Assert.IsNotNull(result);
        }
        [Test]
        public void DeleteBeerType_BeerTypeNotDeletedReturnsExpectedBeerType()
        {
            // Arrange
            var beerType = GetTestBeerType();
            _context.BeerTypes.Add(beerType);
            _context.SaveChanges();
            _context.BeerTypes.Add(beerType);
            _context.SaveChanges();
            _context.BeerTypes.Add(beerType);
            _context.SaveChanges();

            int idDeleted = 2;
            int notDeleted = 3;
            var expected = _beerTypeRepository.Get(notDeleted);
            _beerTypeRepository.Delete(idDeleted);

            //Act
            var actual = _beerTypeRepository.Get(notDeleted);

            //Assert
            Assert.AreEqual(actual,expected);
        }
        [Test]
        public void PutBeerType_AffectedFieldIsChanged()
        {
            // Arrange
            var beerType = GetTestBeerType();
            _context.BeerTypes.Add(beerType);
            _context.SaveChanges();
            string oldName = beerType.BeerTypeName;

            // Act
            beerType = _context.BeerTypes.ToList().LastOrDefault();
            beerType.BeerTypeName = "newName";
            _beerTypeRepository.Put(beerType.BeerTypeID, beerType);
            var actual = _beerTypeRepository.Get(beerType.BeerTypeID).BeerTypeName;

            // Assert
            Assert.AreNotEqual(actual, "Test");
        }
        [Test]
        public void PutBeerType_NonUpdatedBeerTypeHasNoChange()
        {
            var dummyBeerType = GetTestBeerType();
            var originalBeerType = GetTestBeerType();
            originalBeerType.BeerTypeName = "original";
            _context.BeerTypes.Add(dummyBeerType);
            _context.SaveChanges();
            _context.BeerTypes.Add(originalBeerType);
            _context.SaveChanges();

            var updatedBeerType = _context.BeerTypes.ToList().LastOrDefault();
            updatedBeerType.BeerTypeName = "changed";
            _beerTypeRepository.Put(updatedBeerType.BeerTypeID, updatedBeerType);
            
            var actual = _beerTypeRepository.Get(updatedBeerType.BeerTypeID).BeerTypeName; 

            Assert.AreNotEqual(actual, dummyBeerType.BeerTypeName);
            Assert.AreEqual(dummyBeerType.BeerTypeName, _context.BeerTypes.ToList().FirstOrDefault().BeerTypeName);
        }

        [Test]
        public void PostBeerType_ReturnsBeerTypeExists()
        {
            // Arrange
            var beerType = GetTestBeerType();

            // Act
            var preResult = _context.BeerTypes.Any();
            _context.BeerTypes.Add(beerType);
            _context.SaveChanges();

            var postResult = _context.BeerTypes.Any();

            // Assert
            Assert.IsFalse(preResult);
            Assert.IsTrue(postResult);
        }

        private BeerType GetTestBeerType()
        {
            return new BeerType { BeerTypeName = "testbeer" };
        }
    }
}
