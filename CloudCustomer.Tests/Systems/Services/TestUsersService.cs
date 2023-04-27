using CloudCustomer.API.Config;
using CloudCustomer.API.Models;
using CloudCustomer.API.Services;
using CloudCustomer.Tests.Fixtures;
using CloudCustomer.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;

namespace CloudCustomer.Tests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRquest()
        {
            //Arrange
            var expectedResponse = UsersFixtures.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);
            //Act
            await sut.GetAllUsers();
            //Assert
            //Verify HTTP request is made
            handlerMock
                .Protected()
                .Verify(
                    "SendAsync", 
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                );
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnEmptyListOfUsers()
        {
            //Arrange
            var handlerMock = MockHttpMessageHandler<User>.SetuReturn404();
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient, config);
            //Act
            var result = await sut.GetAllUsers();
            //Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnListOfUsersOfExpectedSize ()
        {
            //Arrange
            var expectedResponse = UsersFixtures.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(httpClient,config);
            //Act
            var result = await sut.GetAllUsers();
            //Assert
            result.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {
            //Arrange
            var expectedResponse = UsersFixtures.GetTestUsers();
            var endpoint = "https://TestApi.com/users";
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse, endpoint);

            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UsersService(httpClient, config);
            //Act
            var result = await sut.GetAllUsers();
            var uri = new Uri(endpoint);
            //Assert
            handlerMock
              .Protected()
              .Verify(
                  "SendAsync",
                  Times.Exactly(1),
                  ItExpr.Is<HttpRequestMessage>(
                      req => 
                      req.Method == HttpMethod.Get && req.RequestUri == uri),
                  ItExpr.IsAny<CancellationToken>()
              );

        }
    }
}
