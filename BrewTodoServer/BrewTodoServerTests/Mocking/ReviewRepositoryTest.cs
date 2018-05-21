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
    public class ReviewRepositoryTests
    {
        private DbContext _context;
        private IRepository<Review> _reviewRepository;
        private IRepository<Brewery> _breweryRepository;
        private IRepository<User> _userRepository;

        [SetUp]
        public void SetUp()
        {
            string connStr = ConfigurationManager.ConnectionStrings["BrewTodoDb"].ConnectionString;
            var connection = DbConnectionFactory.CreateTransient();
            _context = new DbContext(connection);
            _reviewRepository = new ReviewRepository(_context);
            _breweryRepository = new BreweryRepository(_context);
            _userRepository = new UserRepository(_context);
        }

        [Test]
        public void GetReview_WithNoId_ReturnsQueryable()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            _breweryRepository.Post(brewery);
            brewery = _breweryRepository.Get().FirstOrDefault();

            User user = DummyUser();
            _userRepository.Post(user);
            user = _userRepository.Get().FirstOrDefault();

            Review review = DummyReview(brewery, user);
            brewery.Reviews = new List<Review>
            {
                review
            };
            _breweryRepository.Put(brewery.BreweryID, brewery);

            // Act
            IQueryable<Review> retrievedReviews = _reviewRepository.Get();

            // Assert
            Assert.AreEqual(retrievedReviews.Count(), 1);
        }

        [Test]
        public void GetReview_WithExistingId_ReturnsReview()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            _breweryRepository.Post(brewery);
            brewery = _breweryRepository.Get().FirstOrDefault();

            User user = DummyUser();
            _userRepository.Post(user);
            user = _userRepository.Get().FirstOrDefault();

            Review review = DummyReview(brewery, user);
            brewery.Reviews = new List<Review>
            {
                review
            };
            _breweryRepository.Put(brewery.BreweryID, brewery);

            // Act
            Review retrievedReview = _reviewRepository.Get().FirstOrDefault();
            
            // Assert
            Assert.IsNotNull(retrievedReview);
            Assert.AreEqual(review.ReviewDescription, retrievedReview.ReviewDescription);
        }

        [Test]
        public void GetReview_WithNonExistentId_ReturnsNull()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            _breweryRepository.Post(brewery);
            brewery = _breweryRepository.Get().FirstOrDefault();

            User user = DummyUser();
            _userRepository.Post(user);
            user = _userRepository.Get().FirstOrDefault();

            Review review = DummyReview(brewery, user);
            brewery.Reviews = new List<Review>
            {
                review
            };
            _breweryRepository.Put(brewery.BreweryID, brewery);

            // Act
            Review retrievedReview = _reviewRepository.Get(3);

            // Assert
            Assert.IsNull(retrievedReview);
        }

        [Test]
        public void DeleteReview_WithNonExistantId_ReturnsFalse()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            _breweryRepository.Post(brewery);
            brewery = _breweryRepository.Get().FirstOrDefault();

            User user = DummyUser();
            _userRepository.Post(user);
            user = _userRepository.Get().FirstOrDefault();

            Review review = DummyReview(brewery, user);
            brewery.Reviews = new List<Review>
            {
                review
            };
            _breweryRepository.Put(brewery.BreweryID, brewery);

            // Act
            bool deleteStatus = _reviewRepository.Delete(3);

            // Assert
            Assert.IsFalse(deleteStatus);
        }

        [Test]
        public void DeleteReview_WithExistantId_ReturnsTrue()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            _breweryRepository.Post(brewery);
            brewery = _breweryRepository.Get().FirstOrDefault();

            User user = DummyUser();
            _userRepository.Post(user);
            user = _userRepository.Get().FirstOrDefault();

            Review review = DummyReview(brewery, user);
            brewery.Reviews = new List<Review>
            {
                review
            };
            _breweryRepository.Put(brewery.BreweryID, brewery);
            brewery = _breweryRepository.Get().FirstOrDefault();
            review = brewery.Reviews.FirstOrDefault();

            // Act
            bool deleteStatus = _reviewRepository.Delete(review.ReviewID);

            // Assert
            Assert.IsTrue(deleteStatus);
        }

        [Test]
        public void PutReview_WithExistantId_ReturnsTrue()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            _breweryRepository.Post(brewery);
            brewery = _breweryRepository.Get().FirstOrDefault();

            User user = DummyUser();
            _userRepository.Post(user);
            user = _userRepository.Get().FirstOrDefault();

            Review review = DummyReview(brewery, user);
            brewery.Reviews = new List<Review>
            {
                review
            };
            _breweryRepository.Put(brewery.BreweryID, brewery);

            // Act
            review = brewery.Reviews.FirstOrDefault();
            string desc = review.ReviewDescription;
            review.ReviewDescription = "this is a different fake review";

            bool result = _reviewRepository.Put(review.ReviewID, review);
            review = _reviewRepository.Get(review.ReviewID);

            // Assert
            Assert.IsTrue(result);
            Assert.AreNotEqual(review.ReviewDescription, desc);
        }

        [Test]
        public void PutReview_WithNonExistantId_ReturnsFalse()
        {
            // Arrange
            Brewery brewery = DummyBrewery();
            _breweryRepository.Post(brewery);
            brewery = _breweryRepository.Get().FirstOrDefault();

            User user = DummyUser();
            _userRepository.Post(user);
            user = _userRepository.Get().FirstOrDefault();

            Review review = DummyReview(brewery, user);
            brewery.Reviews = new List<Review>
            {
                review
            };
            _breweryRepository.Put(brewery.BreweryID, brewery);

            // Act
            review = brewery.Reviews.FirstOrDefault();
            string desc = review.ReviewDescription;
            review.ReviewDescription = "this is a different fake review";

            bool result = _reviewRepository.Put(0, review);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void PostReview()
        {
            Assert.Fail();
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

        private Review DummyReview(Brewery brewery, User user)
        {
            return new Review
            {
                Rating = 5f,
                BreweryID = brewery.BreweryID,
                ReviewDescription = "fake review",
                UserID = user.UserID
            };
        }
    }
}
