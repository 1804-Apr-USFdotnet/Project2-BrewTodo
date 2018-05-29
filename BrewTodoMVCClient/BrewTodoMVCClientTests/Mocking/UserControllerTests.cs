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
    public class UserControllerTests
    {
        UserController controller;
        UserLogic uLogic;
        AccountLogic aLogic;
        private static readonly FormCollection collection;
        private List<UserViewModel> uList;
        private List<Account> aList;
        private TestUserApiMethods uApi;
        private TestAccountApiMethods aApi;

        static UserControllerTests()
        {
            collection = new FormCollection
            {
                { "Username", "test" },
                { "FirstName", "first" },
                { "LastName", "last" },
                { "Password", "password" },
                { "Password2", "password" }
            };
        }

        [SetUp]
        public void SetUp()
        {
            uList = new List<UserViewModel>();
            aList = new List<Account>();
            uApi = new TestUserApiMethods(uList, aList);
            aApi = new TestAccountApiMethods(aList, uList);
            uLogic = new UserLogic(uApi);
            aLogic = new AccountLogic(aApi);
            controller = new UserController(uLogic, aLogic);
        }

        [TearDown]
        public void TearDown()
        {
            uLogic = null;
            controller = null;
        }

        [Test]
        public void Users_Test()
        {
            // Arrange
            List<UserViewModel> list = new List<UserViewModel>();

            // Act
            ViewResult result = controller.Users() as ViewResult;

            // Assert
            Assert.AreEqual(result.ViewData.Model.GetType(), list.GetType());
        }

        [Test]
        public void Create_Test()
        {
            // Arrange

            // Act
            RedirectToRouteResult result = controller.Create(collection) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Create_Test_DuplicateUsername()
        {
            // Arrange
            UserViewModel user = new UserViewModel
            {
                Username = collection["Username"],
                FirstName = collection["FirstName"],
                LastName = collection["LastName"]
            };
            uList.Add(user);

            // Act
            ViewResult result = controller.Create(collection) as ViewResult;

            // Assert
            Assert.AreEqual("Username already exists.", result.ViewBag.UsernameError);
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
            newCollection["Username"] = null;
            controller.ModelState.AddModelError("Username", "cannot be null");

            // Act
            ViewResult result = controller.Create(collection) as ViewResult;

            // Assert
            Assert.AreEqual("Invalid Model State", result.ViewName);
        }

        [Test]
        public void Create_Test_PasswordMismatch()
        {
            // Arrange
            FormCollection newCollection = new FormCollection();
            foreach (var i in collection.AllKeys)
            {
                newCollection.Add(i, collection.GetValue(i).AttemptedValue);
            }
            newCollection["Password2"] = "test";

            // Act
            ViewResult result = controller.Create(newCollection) as ViewResult;

            // Assert
            Assert.AreEqual("Passwords must match.", result.ViewBag.PasswordError);
        }

        [Test]
        public void Edit_Test_ValidInput()
        {
            // Arrange
            UserViewModel oldUser = CreateUserFromForm(collection);
            uLogic.PostUser(oldUser);
            oldUser = uLogic.GetUsers().FirstOrDefault();
            FormCollection newCollection = new FormCollection();
            foreach (var i in collection.AllKeys)
            {
                newCollection.Add(i, collection.GetValue(i).AttemptedValue);
            }
            newCollection["FirstName"] = "newfirst";

            // Act
            RedirectToRouteResult result = controller.Edit(oldUser.UserID, newCollection) as RedirectToRouteResult;

            // Assert
            UserViewModel newUser = uLogic.GetUsers().FirstOrDefault();
            Assert.AreNotEqual(newUser.FirstName, collection["FirstName"]);
        }

        [Test]
        public void Edit_Test_InvalidInput()
        {
            // Arrange
            UserViewModel oldUser = CreateUserFromForm(collection);
            uLogic.PostUser(oldUser);
            oldUser = uLogic.GetUsers().FirstOrDefault();
            FormCollection newCollection = new FormCollection();
            foreach (var i in collection.AllKeys)
            {
                newCollection.Add(i, collection.GetValue(i).AttemptedValue);
            }
            newCollection["Username"] = null;
            controller.ModelState.AddModelError("Username", "cannot be null");

            // Act
            ViewResult result = controller.Create(newCollection) as ViewResult;

            // Assert
            Assert.AreEqual("Invalid Model State", result.ViewName);
        }

        [Test]
        public void Delete_Test_NotLoggedIn()
        {
            // Arrange
            uApi.logIn = false;
            UserViewModel User = CreateUserFromForm(collection);
            uLogic.PostUser(User);
            User = uLogic.GetUsers().FirstOrDefault();

            // Act
            RedirectToRouteResult result = controller.Delete(User.UserID, collection) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [Test]
        public void Delete_Test_LoggedIn()
        {
            // Arrange
            uApi.logIn = true;
            UserViewModel User = CreateUserFromForm(collection);
            uLogic.PostUser(User);
            User = uLogic.GetUsers().FirstOrDefault();

            // Act
            RedirectToRouteResult result = controller.Delete(User.UserID, collection) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Logout", result.RouteValues["action"]);
        }

        [Test]
        public void Details()
        {
            // Arrange
            UserViewModel User = CreateUserFromForm(collection);
            uLogic.PostUser(User);
            User = uLogic.GetUsers().FirstOrDefault();

            // Act
            ViewResult result = controller.Details(User.UserID) as ViewResult;

            // Assert
            Assert.AreEqual(User.GetType(), result.Model.GetType());
        }

        private UserViewModel CreateUserFromForm(FormCollection formData)
        {
            UserViewModel result = new UserViewModel
            {
                UserID = 1,
                FirstName = formData["FirstName"],
                LastName = formData["LastName"],
                Username = formData["Username"]
            };
            return result;
        }
    }
}
