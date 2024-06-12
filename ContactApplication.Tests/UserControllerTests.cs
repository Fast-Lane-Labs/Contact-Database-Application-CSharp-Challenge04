using System.Collections.Generic;
using ContactApplication.Controllers;
using ContactApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Linq;

namespace ContactApplication.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithListOfUsers()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result.Model);
            Assert.Equal(UserController.userlist.Count, ((List<User>)result.Model).Count);
        }



        [Fact]
        public void Details_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var controller = new UserController();
            var invalidId = -1;

            // Act
            var result = controller.Details(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_WithInvalidModel_ReturnsViewResult_WithModel()
        {
            // Arrange
            var controller = new UserController();
            controller.ModelState.AddModelError("Name", "Required");
            var newUser = new User { Id = 2, Email = "jane.smith@example.com" };

            // Act
            var result = controller.Create(newUser) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result.Model);
            Assert.Equal(newUser, result.Model);
        }

        [Fact]
        public void Create_WithValidUser_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var newUser = new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" };
            var initialUserCount = UserController.userlist.Count;

            // Act
            var result = controller.Create(newUser) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal(initialUserCount + 1, UserController.userlist.Count);
            Assert.Contains(newUser, UserController.userlist);
        }

        [Fact]
        public void Edit_Get_WithValidId_ReturnsViewResult_WithUser()
        {
            // Arrange
            var controller = new UserController();
            var validId = 1;

            // Act
            var result = controller.Edit(validId) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result.Model);
            Assert.Equal(validId, ((User)result.Model).Id);
        }

        [Fact]
        public void Edit_Get_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var controller = new UserController();
            var invalidId = -1;

            // Act
            var result = controller.Edit(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Get_WithValidId_ReturnsViewResult_WithUser()
        {
            // Arrange
            var controller = new UserController();
            var validId = 1;

            // Act
            var result = controller.Delete(validId) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result.Model);
            Assert.Equal(validId, ((User)result.Model).Id);
        }

        [Fact]
        public void Delete_Get_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var controller = new UserController();
            var invalidId = -1;

            // Act
            var result = controller.Delete(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Post_WithValidId_RemovesUserAndRedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userIdToDelete = 1;
            var initialUserCount = UserController.userlist.Count;

            // Act
            var result = controller.Delete(userIdToDelete, null) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal(initialUserCount - 1, UserController.userlist.Count);
        }

        [Fact]
        public void Delete_Post_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var controller = new UserController();
            var invalidId = -1;

            // Act
            var result = controller.Delete(invalidId, null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_WithValidIdAndUser_UpdatesUserAndRedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userIdToEdit = 1;
            var updatedUser = new User { Id = 1, Name = "John Doe Updated", Email = "john.doe.updated@example.com" };

            // Act
            var result = controller.Edit(userIdToEdit, updatedUser) as RedirectToActionResult;
            var editedUser = UserController.userlist.FirstOrDefault(u => u.Id == userIdToEdit);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.NotNull(editedUser);
            Assert.Equal(updatedUser.Name, editedUser.Name);
            Assert.Equal(updatedUser.Email, editedUser.Email);
        }

        [Fact]
        public void Edit_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var controller = new UserController();
            var invalidId = -1;
            var updatedUser = new User { Id = 1, Name = "John Doe Updated", Email = "john.doe.updated@example.com" };

            // Act
            var result = controller.Edit(invalidId, updatedUser);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}