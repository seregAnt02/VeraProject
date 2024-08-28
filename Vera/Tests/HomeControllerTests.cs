using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Vera.Controllers;
using Xunit;

namespace Vera.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexViewDataMessage()
        {
            // Arrange
            HomeController controller = new HomeController(new Infrastructure.VeraContext());

            // Act
            ViewResult? result = controller.Index() as ViewResult;

            // Assert
            Assert.Equal("Hello world!", result?.ViewData["Message"]);
        }

        [Fact]
        void IndexViewDateNull()
        {
            //Arrange
            HomeController controller = new HomeController(new Infrastructure.VeraContext());

            //Act
            ViewResult?  result = controller.Index() as ViewResult;

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexViewNameEqualIndex()
        {
            // Arrange
            HomeController controller = new HomeController(new Infrastructure.VeraContext());
            // Act
            ViewResult? result = controller.Index() as ViewResult;
            // Assert
            Assert.Equal("Index", result?.ViewName);
        }

        [Fact]
        public void IndexViewDateNullAddFormsDelCancel()
        {
            //Arrange
            HomeController controller = new HomeController(new Infrastructure.VeraContext());

            //Act
            IActionResult? result = controller.AddFormsDelCancel();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexViewDateNullMeasuringAdd()
        {
            //Arrange
            HomeController controller = new HomeController(new Infrastructure.VeraContext());

            //Act
            IActionResult? result = controller.MeasuringAdd();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexViewDateNullTableAddUpdate()
        {
            //Arrange
            HomeController controller = new HomeController(new Infrastructure.VeraContext());

            //Act
            IActionResult? result = controller.TableAddUpdate();

            //Assert
            Assert.NotNull(result);
        }
    }
}
