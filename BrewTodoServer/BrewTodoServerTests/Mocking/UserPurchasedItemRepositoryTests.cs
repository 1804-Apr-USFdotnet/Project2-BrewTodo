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
    public class UserPurchasedItemRepositoryTests
    {
        private DbContext _context;
        private IRepository<UserPurchasedItem> _userPurchasedItemRepository;
        private IRepository<Brewery> _breweryRepository;
        private IRepository<User> _userRepository;
        private IRepository<Beer> _beerRepository;

        [SetUp]
        public void SetUp()
        {
            string connStr = ConfigurationManager.ConnectionStrings["BrewTodoDb"].ConnectionString;
            var connection = DbConnectionFactory.CreateTransient();
            _context = new DbContext(connection);
            _userPurchasedItemRepository = new UserPurchasedItemRepository(_context);
            _breweryRepository = new BreweryRepository(_context);
            _userRepository = new UserRepository(_context);
            _beerRepository = new BeerRepository(_context);
        }

        [Test]
        public void GetUserPurchasedItem_WithNoId_ReturnsQueryable()
        {
            // Arrange
            UserPurchasedItem purchasedItems = DummyUserPurchasedItem(DummyBrewery(), DummyUser());
            _context.UserPurchasedItems.Add(purchasedItems);
            _context.SaveChanges();

            // Act
            IQueryable<UserPurchasedItem> retrievedUserPurchasedItems = _userPurchasedItemRepository.Get();

            // Assert
            Assert.AreEqual(retrievedUserPurchasedItems.Count(), 1);
        }

        [Test]
        public void GetUserPurchasedItem_WithExistingId_ReturnsUserPurchasedItem()
        {
            // Arrange
            UserPurchasedItem purchasedItems = DummyUserPurchasedItem(DummyBrewery(), DummyUser());
            _context.UserPurchasedItems.Add(purchasedItems);
            _context.SaveChanges();
            int id = _context.UserPurchasedItems.ToList().FirstOrDefault().UserPurchasedItemID;
            bool hasPurchasedShirt = purchasedItems.PurchasedTShirt;

            // Act
            UserPurchasedItem result = _userPurchasedItemRepository.Get(id);
            
            // Assert
            Assert.AreEqual(hasPurchasedShirt, result.PurchasedTShirt);
        }

        [Test]
        public void GetUserPurchasedItem_WithNonExistentId_ReturnsNull()
        {
            // Arrange
            UserPurchasedItem purchasedItems = DummyUserPurchasedItem(DummyBrewery(), DummyUser());
            _context.UserPurchasedItems.Add(purchasedItems);
            _context.SaveChanges();

            // Act
            var result = _userPurchasedItemRepository.Get(5);

            // Assert
            Assert.IsNull(result);
        }
        
        [Test]
        public void DeleteUserPurchasedItem_WithNonExistantId_ReturnsFalse()
        {
            // Arrange
            UserPurchasedItem purchasedItems = DummyUserPurchasedItem(DummyBrewery(), DummyUser());
            _context.UserPurchasedItems.Add(purchasedItems);
            _context.SaveChanges();

            // Act
            bool deleteStatus = _userPurchasedItemRepository.Delete(5);

            // Assert
            Assert.IsFalse(deleteStatus);
        }

        [Test]
        public void DeleteUserPurchasedItem_WithExistantId_ReturnsTrue()
        {
            // Arrange
            UserPurchasedItem purchasedItems = DummyUserPurchasedItem(DummyBrewery(), DummyUser());
            _context.UserPurchasedItems.Add(purchasedItems);
            _context.SaveChanges();
            int id = _context.UserPurchasedItems.ToList().FirstOrDefault().UserPurchasedItemID;

            // Act
            bool deleteStatus = _userPurchasedItemRepository.Delete(id);

            // Assert
            Assert.IsTrue(deleteStatus);
        }

        [Test]
        public void PutUserPurchasedItem_WithExistantId_ReturnsTrue()
        {
            // Arrange
            UserPurchasedItem purchasedItems = DummyUserPurchasedItem(DummyBrewery(), DummyUser());
            _context.UserPurchasedItems.Add(purchasedItems);
            _context.SaveChanges();

            // Act
            UserPurchasedItem newPurchasedItem = _context.UserPurchasedItems.ToList().FirstOrDefault();
            newPurchasedItem.PurchasedMug = true;
            bool result = _userPurchasedItemRepository.Put(newPurchasedItem.UserPurchasedItemID, newPurchasedItem);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void PutUserPurchasedItem_WithNonExistantId_ReturnsFalse()
        {
            // Arrange
            UserPurchasedItem purchasedItems = DummyUserPurchasedItem(DummyBrewery(), DummyUser());
            _context.UserPurchasedItems.Add(purchasedItems);
            _context.SaveChanges();

            // Act
            UserPurchasedItem newPurchasedItem = _context.UserPurchasedItems.ToList().FirstOrDefault();
            newPurchasedItem.PurchasedMug = true;
            bool result = _userPurchasedItemRepository.Put(5, newPurchasedItem);

            // Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void PostUserPurchasedItem_ReturnsUserPurchasedItemListCount()
        {
            // Arrange
            UserPurchasedItem purchasedItems = DummyUserPurchasedItem(DummyBrewery(), DummyUser());
            var pre = _context.UserPurchasedItems.Any();

            // Act
            _userPurchasedItemRepository.Post(purchasedItems);

            // Assert
            var post = _context.UserPurchasedItems.Any();
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

        private UserPurchasedItem DummyUserPurchasedItem(Brewery brewery, User user)
        {
            return new UserPurchasedItem
            {
                User = user,
                Brewery = brewery,
                PurchasedGrowler = false,
                PurchasedMug = false,
                PurchasedTShirt = true
            };
        }
    }
}
