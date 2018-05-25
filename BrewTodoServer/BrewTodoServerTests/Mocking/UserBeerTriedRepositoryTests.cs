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
    public class UserBeerTriedRepositoryTests
    {
        private DbContext _context;
        private IRepository<UserBeerTried> _userBeerTriedRepository;
        private IRepository<Brewery> _breweryRepository;
        private IRepository<User> _userRepository;
        private IRepository<Beer> _beerRepository;

        [SetUp]
        public void SetUp()
        {
            string connStr = ConfigurationManager.ConnectionStrings["BrewTodoDb"].ConnectionString;
            var connection = DbConnectionFactory.CreateTransient();
            _context = new DbContext(connection);
            _userBeerTriedRepository = new UserBeerTriedRepository(_context);
            _breweryRepository = new BreweryRepository(_context);
            _userRepository = new UserRepository(_context);
            _beerRepository = new BeerRepository(_context);
        }

        [Test]
        public void GetUserBeerTried_WithNoId_ReturnsQueryable()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            Beer beer = DummyBeer();
            beer.Brewery = brewery;
            UserBeerTried beerTried = DummyUserBeerTried(beer, DummyUser());
            _context.UserBeersTried.Add(beerTried);
            _context.SaveChanges();

            // Act
            IQueryable<UserBeerTried> retrievedUserBeersTried = _userBeerTriedRepository.Get();

            // Assert
            Assert.AreEqual(retrievedUserBeersTried.Count(), 1);
        }

        [Test]
        public void GetUserBeerTried_WithExistingId_ReturnsUserBeerTried()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            Beer beer = DummyBeer();
            beer.Brewery = brewery;
            UserBeerTried beerTried = DummyUserBeerTried(beer, DummyUser());
            _context.UserBeersTried.Add(beerTried);
            _context.SaveChanges();
            int id = _context.UserBeersTried.ToList().FirstOrDefault().UserBeerTriedID;

            // Act
            UserBeerTried retrievedUserBeerTried = _userBeerTriedRepository.Get(id);

            // Assert
            Assert.AreEqual(retrievedUserBeerTried.Beer.BeerName, beerTried.Beer.BeerName);
        }

        [Test]
        public void GetUserBeerTried_WithNonExistentId_ReturnsNull()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            Beer beer = DummyBeer();
            beer.Brewery = brewery;
            UserBeerTried beerTried = DummyUserBeerTried(beer, DummyUser());
            _context.UserBeersTried.Add(beerTried);
            _context.SaveChanges();

            // Act
            UserBeerTried retrievedUserBeerTried = _userBeerTriedRepository.Get(5);

            // Assert
            Assert.IsNull(retrievedUserBeerTried);
        }

        [Test]
        public void DeleteUserBeerTried_WithNonExistantId_ReturnsFalse()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            Beer beer = DummyBeer();
            beer.Brewery = brewery;
            UserBeerTried beerTried = DummyUserBeerTried(beer, DummyUser());
            _context.UserBeersTried.Add(beerTried);
            _context.SaveChanges();

            // Act
            bool deleteStatus = _userBeerTriedRepository.Delete(3);

            // Assert
            Assert.IsFalse(deleteStatus);
        }

        [Test]
        public void DeleteUserBeerTried_WithExistantId_ReturnsTrue()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            Beer beer = DummyBeer();
            beer.Brewery = brewery;
            UserBeerTried beerTried = DummyUserBeerTried(beer, DummyUser());
            _context.UserBeersTried.Add(beerTried);
            _context.SaveChanges();
            int id = _context.UserBeersTried.ToList().FirstOrDefault().UserBeerTriedID;

            // Act
            bool deleteStatus = _userBeerTriedRepository.Delete(id);

            // Assert
            Assert.IsTrue(deleteStatus);
        }

        [Test]
        public void PutUserBeerTried_WithExistantId_ReturnsTrue()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            Beer beer = DummyBeer();
            beer.Brewery = brewery;
            UserBeerTried beerTried = DummyUserBeerTried(beer, DummyUser());
            _context.UserBeersTried.Add(beerTried);
            _context.SaveChanges();

            // Act
            UserBeerTried retrieved = _context.UserBeersTried.ToList().FirstOrDefault();
            retrieved.Beer.BeerName = "changedName";
            bool result = _userBeerTriedRepository.Put(retrieved.UserBeerTriedID, retrieved);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void PutUserBeerTried_WithNonExistantId_ReturnsFalse()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            Beer beer = DummyBeer();
            beer.Brewery = brewery;
            UserBeerTried beerTried = DummyUserBeerTried(beer, DummyUser());
            _context.UserBeersTried.Add(beerTried);
            _context.SaveChanges();

            // Act
            UserBeerTried retrieved = _context.UserBeersTried.ToList().FirstOrDefault();
            retrieved.Beer.BeerName = "changedName";
            bool result = _userBeerTriedRepository.Put(5, retrieved);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void PostUserBeerTried_ReturnsUserBeerTriedListCount()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            Beer beer = DummyBeer();
            beer.Brewery = brewery;
            UserBeerTried beerTried = DummyUserBeerTried(beer, DummyUser());
            var pre = _context.UserBeersTried.Any();

            // Act
            _userBeerTriedRepository.Post(beerTried);

            // Assert
            var post = _context.UserBeersTried.Any();
            Assert.IsFalse(pre);
            Assert.IsTrue(post);
        }

        private Brewery DummyBrewery()
        {
            State state = new State
            {
                StateID = 1,
                StateAbbr = "Fl"
            };

            return new Brewery
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
        }

        private User DummyUser()
        {
            return new User
            {
                Username = "fakeuser"
            };
        }

        private Beer DummyBeer()
        {
            BeerType type = new BeerType
            {
                BeerTypeID = 0,
                BeerTypeName = "testType"
            };
            return new Beer
            {
                ABV = 5.0,
                BeerName = "test",
                BeerType = type,
                Description = "testBeer"
            };
        }

        private UserBeerTried DummyUserBeerTried(Beer beer, User user)
        {
            return new UserBeerTried
            {
                User = user,
                Beer = beer
            };
        }
    }
}
