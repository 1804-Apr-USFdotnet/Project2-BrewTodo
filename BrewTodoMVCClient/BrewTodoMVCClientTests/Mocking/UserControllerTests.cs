using BrewTodoMVCClient;
using BrewTodoMVCClient.Models;
using Effort;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;

namespace BrewTodoMVCClientTests.Mocking
{
    [TestFixture]
    public class UserControllerTests
    {
        //private DbContext _context;
        //private IRepository<User> _userRepository;

        [SetUp]
        public void SetUp()
        {
            string connStr = ConfigurationManager.ConnectionStrings["BrewTodoDb"].ConnectionString;
            var connection = DbConnectionFactory.CreateTransient();
            //_context = new DbContext(connection);
            //_userRepository = new UserRepository(_context);
        }

        [Test]
        public void Create()
        {
            // Arrange

            // Act

            // Assert
        }

        [Test]
        public void Edit()
        {
            // Arrange

            // Act

            // Assert
        }

        [Test]
        public void Delete()
        {
            // Arrange

            // Act

            // Assert
        }

        [Test]
        public void Details()
        {
            // Arrange

            // Act

            // Assert
        }

        [Test]
        public void Index()
        {
            // Arrange

            // Act

            // Assert
        }

        [Test]
        public void Users()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
