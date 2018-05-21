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
    public class UserRepositoryTests
    {
        private DbContext _context;
        private IRepository<User> _userRepository;

        [SetUp]
        public void SetUp()
        {
            string connStr = ConfigurationManager.ConnectionStrings["BrewTodoDb"].ConnectionString;
            var connection = DbConnectionFactory.CreateTransient();
            _context = new DbContext(connection);
            _userRepository = new UserRepository(_context);
        }

        [Test]
        public void GetUser_WithNoId_ReturnsQueryable()
        {
            // Arrange
            User user = DummyUser();
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            IQueryable<User> retrievedUsers = _userRepository.Get();

            // Assert
            Assert.AreEqual(retrievedUsers.Count(), 1);
        }

        [Test]
        public void GetUser_WithExistingId_ReturnsUser()
        {
            // Arrange
            User user = DummyUser();
            _context.Users.Add(user);
            _context.SaveChanges();

            int id = _context.Users.FirstOrDefault().UserID;

            // Act
            User retrievedUser = _userRepository.Get(id);

            // Assert
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(user.Username, retrievedUser.Username);
        }

        [Test]
        public void GetUser_WithNonExistentId_ReturnsNull()
        {
            // Arrange
            User user = DummyUser();
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            User retrievedUser = _userRepository.Get(5);

            // Assert
            Assert.IsNull(retrievedUser);
        }

        [Test]
        public void DeleteUser_WithNonExistantId_ReturnsFalse()
        {
            // Arrange
            User user = DummyUser();
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            bool deleteStatus = _userRepository.Delete(3);

            // Assert
            Assert.IsFalse(deleteStatus);
        }

        [Test]
        public void DeleteUser_WithExistantId_ReturnsTrue()
        {
            // Arrange
            User user = DummyUser();
            _context.Users.Add(user);
            _context.SaveChanges();

            int id = _context.Users.FirstOrDefault().UserID;

            // Act
            bool deleteStatus = _userRepository.Delete(id);

            // Assert
            Assert.IsTrue(deleteStatus);
        }

        [Test]
        public void PutReview_WithExistantId_ReturnsTrue()
        {
            // Arrange
            User user = DummyUser();
            _context.Users.Add(user);
            _context.SaveChanges();
            user = _context.Users.FirstOrDefault();

            // Act
            string firstName = user.FirstName;
            user.FirstName = "first";

            bool result = _userRepository.Put(user.UserID, user);
            user = _context.Users.Find(user.UserID);

            // Assert
            Assert.IsTrue(result);
            Assert.AreNotEqual(firstName, user.FirstName);
        }

        [Test]
        public void PutReview_WithNonExistantId_ReturnsFalse()
        {
            // Arrange
            User user = DummyUser();
            _context.Users.Add(user);
            _context.SaveChanges();
            user = _context.Users.FirstOrDefault();

            // Act
            user.FirstName = "first";
            user.LastName = "last";

            bool result = _userRepository.Put(0, user);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void PostReview_ReturnsReviewListCount()
        {
            // Arrange
            User user = DummyUser();
            int count = _context.Users.ToList().Count;

            // Act
            _userRepository.Post(user);

            // Assert
            Assert.IsTrue(count + 1 == _context.Users.ToList().Count);
        }

        private User DummyUser()
        {
            return new User
            {
                Username = "fakeuser"
            };
        }
    }
}
