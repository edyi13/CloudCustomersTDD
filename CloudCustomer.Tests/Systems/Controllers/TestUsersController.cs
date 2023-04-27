using CloudCustomer.API.Controllers;
using CloudCustomer.API.Models;
using CloudCustomer.API.Services;
using CloudCustomer.Tests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomer.Tests.Systems.Controllers
{
    public class TestUsersController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            // Arrange
            var mockUsersService = new Mock<IUsersService>();
            mockUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(UsersFixtures.GetTestUsers());
            var sut = new UsersController(mockUsersService.Object);
            // Act
            var result = (OkObjectResult) await sut.Get();
            // Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokesUserrServicesExactlyOnce()
        {
            // Arrange
            var mockUsersService = new Mock<IUsersService>();
            mockUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());
            var sut = new UsersController(mockUsersService.Object);
            // Act
            var result = await sut.Get();
            // Assert
            mockUsersService.Verify(
                service => service.GetAllUsers(), 
                Times.Once());
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnListOfUsers()
        {
            // Arrange
            var mockUsersService = new Mock<IUsersService>();
            mockUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(UsersFixtures.GetTestUsers());
            var sut = new UsersController(mockUsersService.Object);
            // Act
            var result = await sut.Get();
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult) result;
            objectResult.Value.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task Get_OnNoUsersFound_Return4004()
        {
            // Arrange
            var mockUsersService = new Mock<IUsersService>();
            mockUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());
            var sut = new UsersController(mockUsersService.Object);
            // Act
            var result = await sut.Get();
            // Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }


    }
}