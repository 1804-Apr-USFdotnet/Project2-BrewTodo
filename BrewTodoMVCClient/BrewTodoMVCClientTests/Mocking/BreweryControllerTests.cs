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
    public class BreweryControllerTests
    {
        BreweryController controller;
        BreweryLogic logic;
        private static readonly FormCollection collection;

        static BreweryControllerTests()
        {
            collection = new FormCollection
            {
                { "Name", "test" },
                { "Description", "test" },
                { "ImageURL", "test.jpg" },
                { "Address", "test address" },
                { "ZipCode", "12345" },
                { "PhoneNumber", "555-555-5555" },
                { "BusinessHours", "12PM-12AM" },
                { "HasTShirt", "false" },
                { "HasMug", "false" },
                { "HasGrowler", "false" },
                { "HasFood", "false" }
            };
        }

        [SetUp]
        public void SetUp()
        {
            logic = new BreweryLogic(new TestBreweryApiMethods());
            controller = new BreweryController(logic);
        }

        [TearDown]
        public void TearDown()
        {
            controller = null;
        }

        [Test]
        public void Breweries_Test()
        {
            // Arrange
            List<BreweryViewModel> list = new List<BreweryViewModel>();

            // Act
            ViewResult result = controller.Breweries() as ViewResult;

            // Assert
            Assert.AreEqual(result.ViewData.Model.GetType(), list.GetType());
        }

        [Test]
        public void Create_Test_ValidInput()
        {
            // Arrange

            // Act
            RedirectToRouteResult result = controller.Create(collection) as RedirectToRouteResult;
            
            // Assert
            Assert.AreEqual("Breweries", result.RouteValues["action"]);
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
            newCollection["Name"] = null;
            controller.ModelState.AddModelError("Name", "cannot be null");
            
            // Act
            ViewResult result = controller.Create(collection) as ViewResult;

            // Assert
            Assert.AreEqual("Invalid Model State", result.ViewName);
        }

        [Test]
        public void Edit_Test_ValidInput()
        {
            // Arrange
            BreweryViewModel oldBrewery = CreateBreweryFromForm(collection);
            logic.PostBrewery(oldBrewery);
            oldBrewery = logic.GetBreweries().FirstOrDefault();
            FormCollection newCollection = new FormCollection();
            foreach (var i in collection.AllKeys)
            {
                newCollection.Add(i, collection.GetValue(i).AttemptedValue);
            }
            newCollection["Description"] = "new description";

            // Act
            RedirectToRouteResult result = controller.Edit(oldBrewery.BreweryID, newCollection) as RedirectToRouteResult;

            // Assert
            BreweryViewModel newBrewery = logic.GetBreweries().FirstOrDefault();
            Assert.AreNotEqual(newBrewery.Description, collection["Description"]);
        }

        [Test]
        public void Edit_Test_InvalidInput()
        {
            // Arrange
            BreweryViewModel oldBrewery = CreateBreweryFromForm(collection);
            logic.PostBrewery(oldBrewery);
            oldBrewery = logic.GetBreweries().FirstOrDefault();
            FormCollection newCollection = new FormCollection();
            foreach (var i in collection.AllKeys)
            {
                newCollection.Add(i, collection.GetValue(i).AttemptedValue);
            }
            newCollection["Name"] = null;
            controller.ModelState.AddModelError("Name", "cannot be null");

            // Act
            ViewResult result = controller.Create(newCollection) as ViewResult;

            // Assert
            Assert.AreEqual("Invalid Model State", result.ViewName);
        }

        [Test]
        public void Delete_Test_ValidInput()
        {
            // Arrange
            BreweryViewModel brewery = CreateBreweryFromForm(collection);
            logic.PostBrewery(brewery);
            brewery = logic.GetBreweries().FirstOrDefault();

            // Act
            RedirectToRouteResult result = controller.Delete(brewery.BreweryID, collection) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Breweries", result.RouteValues["action"]);
        }

        [Test]
        public void Details()
        {
            // Arrange
            BreweryViewModel brewery = CreateBreweryFromForm(collection);
            logic.PostBrewery(brewery);
            brewery = logic.GetBreweries().FirstOrDefault();

            // Act
            ViewResult result = controller.Details(brewery.BreweryID) as ViewResult;

            // Assert
            Assert.AreEqual(brewery.GetType(), result.Model.GetType());
        }

        private BreweryViewModel CreateBreweryFromForm(FormCollection formData)
        {
            BreweryViewModel result = new BreweryViewModel
            {
                BreweryID = 1,
                Name = formData["Name"],
                Description = formData["Description"],
                ImageURL = formData["ImageURL"],
                Address = formData["Address"],
                ZipCode = formData["ZipCode"],
                PhoneNumber = formData["PhoneNumber"],
                BusinessHours = formData["BusinessHours"],
                HasTShirt = Convert.ToBoolean(formData["HasTShirt"].Split(',')[0]),
                HasMug = Convert.ToBoolean(formData["HasMug"].Split(',')[0]),
                HasGrowler = Convert.ToBoolean(formData["HasGrowler"].Split(',')[0]),
                HasFood = Convert.ToBoolean(formData["HasFood"].Split(',')[0]),
                AverageRating = 0,
                State = new State
                {
                    StateID = 10,
                    StateAbbr = "Fl"
                }
            };
            return result;
        }
    }
}
