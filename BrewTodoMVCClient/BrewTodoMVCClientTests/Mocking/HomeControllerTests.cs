using BrewTodoMVCClient;
using BrewTodoMVCClient.Controllers;
using BrewTodoMVCClient.Models;
using Effort;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web.Mvc;

namespace BrewTodoMVCClientTests.Mocking
{
    [TestFixture]
    public class HomeControllerTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Index()
        {
            // Arrange
            //HomeController controller = new HomeController();

            // Act
            //ViewResult result = controller.Index() as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
        }

        [Test]
        public void About()
        {
            // Arrange
            //HomeController controller = new HomeController();

            // Act
            //ViewResult result = controller.About() as ViewResult;

            // Assert
            //Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [Test]
        public void Contact()
        {
            // Arrange
            //HomeController controller = new HomeController();

            // Act
            //ViewResult result = controller.Contact() as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
        }
    }
}
