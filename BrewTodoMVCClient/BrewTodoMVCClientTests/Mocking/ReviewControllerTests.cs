using BrewTodoMVCClient;
using BrewTodoMVCClient.Controllers;
using BrewTodoMVCClient.Models;
using Effort;
using NUnit.Framework;
using System.Configuration;
using System.Linq;
using BrewTodoMVCClientTests.DummyClasses;
using System.Web.Mvc;
using System.Collections.Generic;
using BrewTodoMVCClient.Logic;
using System;

namespace BrewTodoMVCClientTests.Mocking
{
    [TestFixture]
    public class ReviewControllerTests
    {
        private ReviewController controller;
        private ReviewLogic rLogic;
        private BreweryLogic bLogic;
        private static readonly FormCollection collection;
        private static readonly BreweryViewModel brewery;
        private static readonly UserViewModel user;

        static ReviewControllerTests()
        {
            collection = new FormCollection
            {
                { "ReviewDescription", "test" },
                { "Rating", "0" },
                { "ReviewID", "1" }
            };
            brewery = new BreweryViewModel
            {
                BreweryID = 1,
                Name = "test",
                Description = "test",
                ImageURL = "test.jpg",
                Address = "test address",
                ZipCode = "12345",
                PhoneNumber = "555-555-5555",
                BusinessHours = "12PM-12AM",
                HasTShirt = false,
                HasMug = false,
                HasGrowler = false,
                HasFood = false,
                AverageRating = 0,
                State = new State
                {
                    StateID = 10,
                    StateAbbr = "Fl"
                }
            };
            user = new UserViewModel
            {
                Username = "Dummy",
                UserID = 1
            };
        }

        [SetUp]
        public void SetUp()
        {
            rLogic = new ReviewLogic(new TestReviewApiMethods());
            bLogic = new BreweryLogic(new TestBreweryApiMethods());
            bLogic.PostBrewery(brewery);
            controller = new ReviewController(rLogic, bLogic);
        }

        [TearDown]
        public void TearDown()
        {
            controller = null;
        }

        [Test]
        public void Create_Test_ValidInput()
        {
            // Arrange
            int id = bLogic.GetBreweries().LastOrDefault().BreweryID;

            // Act
            RedirectToRouteResult result = controller.Create(collection, id) as RedirectToRouteResult;
            
            // Assert
            Assert.AreEqual("Details", result.RouteValues["action"]);
        }

        [Test]
        public void Create_Test_InvalidInput()
        {
            // Arrange
            FormCollection newCollection = new FormCollection();
            foreach (var i in collection.AllKeys)
            {
                newCollection.Add(i, collection.GetValue(i).AttemptedValue);
            }
            newCollection["ReviewID"] = null;
            controller.ModelState.AddModelError("Review", "cannot be null");
            
            // Act
            ViewResult result = controller.Create(collection, 0) as ViewResult;

            // Assert
            Assert.AreEqual("Invalid Model State", result.ViewName);
        }

        [Test]
        public void Edit_Test_ValidInput()
        {
            // Arrange
            ReviewViewModel oldReview = CreateReviewFromForm(collection);
            rLogic.PostReview(oldReview);
            oldReview = rLogic.GetReviews().FirstOrDefault();
            FormCollection newCollection = new FormCollection
            {
                { "BreweryID", brewery.BreweryID.ToString()},
                { "UserID", user.UserID.ToString() }
            };
            foreach (var i in collection.AllKeys)
            {
                newCollection.Add(i, collection.GetValue(i).AttemptedValue);
            }
            newCollection.Set("ReviewDescription", "new description");

            // Act
            RedirectToRouteResult result = controller.Edit(oldReview.ReviewID, newCollection) as RedirectToRouteResult;

            // Assert
            ReviewViewModel newReview = rLogic.GetReviews().FirstOrDefault();
            Assert.AreNotEqual(newReview.ReviewDescription, collection["ReviewDescription"]);
        }

        [Test]
        public void Edit_Test_InvalidInput()
        {
            // Arrange
            ReviewViewModel oldReview = CreateReviewFromForm(collection);
            rLogic.PostReview(oldReview);
            oldReview = rLogic.GetReviews().FirstOrDefault();
            FormCollection newCollection = new FormCollection();
            foreach (var i in collection.AllKeys)
            {
                newCollection.Add(i, collection.GetValue(i).AttemptedValue);
            }
            newCollection["Name"] = null;
            controller.ModelState.AddModelError("Name", "cannot be null");
            int id = rLogic.GetReviews().LastOrDefault().ReviewID;

            // Act
            ViewResult result = controller.Edit(id, newCollection) as ViewResult;

            // Assert
            Assert.AreEqual("Invalid Model State", result.ViewName);
        }

        [Test]
        public void Delete_Test_ValidInput()
        {
            // Arrange
            int breweryId = bLogic.GetBreweries().LastOrDefault().BreweryID;
            ReviewViewModel review = CreateReviewFromForm(collection);
            rLogic.PostReview(review);
            review = rLogic.GetReviews().FirstOrDefault();

            // Act
            RedirectToRouteResult result = controller.Delete(review.ReviewID, collection) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Details", result.RouteValues["action"]);
        }

        [Test]
        public void Details()
        {/*
            // Arrange
            ReviewViewModel Review = CreateReviewFromForm(collection);
            rLogic.PostReview(Review);
            Review = rLogic.GetReviews().FirstOrDefault();

            // Act
            ViewResult result = controller.Details(Review.ReviewID) as ViewResult;

            // Assert
            Assert.AreEqual(Review.GetType(), result.Model.GetType());
        */}

        private ReviewViewModel CreateReviewFromForm(FormCollection formData)
        {
            ReviewViewModel result = new ReviewViewModel
            {
                Brewery = brewery,
                BreweryID = brewery.BreweryID,
                User = user,
                UserID = user.UserID,
                Rating = int.Parse(formData["Rating"]),
                ReviewDescription = formData["ReviewDescription"],
                ReviewID = int.Parse(formData["ReviewID"])
            };
            return result;
        }
    }
}
