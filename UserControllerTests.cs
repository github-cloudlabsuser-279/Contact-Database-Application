using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void Index_ReturnsViewWithListOfUsers()
        {
            // Arrange
            var controller = new UserController();
            var user1 = new User { Id = 1, Name = "John", Email = "john@example.com" };
            var user2 = new User { Id = 2, Name = "Jane", Email = "jane@example.com" };
            UserController.userlist = new List<User> { user1, user2 };

            // Act
            var result = controller.Index() as ViewResult;
            var model = result.Model as List<User>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, model.Count);
            Assert.IsTrue(model.Any(u => u.Id == 1 && u.Name == "John" && u.Email == "john@example.com"));
            Assert.IsTrue(model.Any(u => u.Id == 2 && u.Name == "Jane" && u.Email == "jane@example.com"));
        }

        [TestMethod]
        public void Details_WithValidId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1, Name = "John", Email = "john@example.com" };
            UserController.userlist = new List<User> { user };

            // Act
            var result = controller.Details(1) as ViewResult;
            var model = result.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user, model);
        }

        [TestMethod]
        public void Details_WithInvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>();

            // Act
            var result = controller.Details(1) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_WithValidUser_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1, Name = "John", Email = "john@example.com" };
            UserController.userlist = new List<User>();

            // Act
            var result = controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Edit_WithValidIdAndUser_UpdatesUserAndRedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1, Name = "John", Email = "john@example.com" };
            UserController.userlist = new List<User> { user };

            // Act
            var result = controller.Edit(1, new User { Id = 1, Name = "Jane", Email = "jane@example.com" }) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Jane", user.Name);
            Assert.AreEqual("jane@example.com", user.Email);
        }

        [TestMethod]
        public void Edit_WithInvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>();

            // Act
            var result = controller.Edit(1, new User { Id = 1, Name = "Jane", Email = "jane@example.com" }) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_WithValidId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1, Name = "John", Email = "john@example.com" };
            UserController.userlist = new List<User> { user };

            // Act
            var result = controller.Delete(1) as ViewResult;
            var model = result.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user, model);
        }

        [TestMethod]
        public void Delete_WithInvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>();

            // Act
            var result = controller.Delete(1) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
