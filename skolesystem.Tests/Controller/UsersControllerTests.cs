using FakeItEasy;
using skolesystem.DTOs;
using skolesystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using skolesystem.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace skolesystem.Tests.Controller
{
    public class UsersControllerTests
    {
        private readonly UserController _sut;
        private readonly Mock<IUsersService> _usersService = new();

        public UsersControllerTests()
        {
            _sut = new UserController(context: null, _usersService.Object);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            List<UserReadDto> users = new List<UserReadDto>
    {
        new UserReadDto
        {
            user_id = 1,
            surname = "Doe",
            email = "john.doe@example.com",
            // Populate other properties as needed
        },
        new UserReadDto
        {
            user_id = 2,
            surname = "Smith",
            email = "jane.smith@example.com",
            // Populate other properties as needed
        }
    };

            _usersService.Setup(s => s.GetAllUsers()).Returns(Task.FromResult<IEnumerable<UserReadDto>>(users));

            // Act
            var result = await _sut.GetUsers();

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<UserReadDto>>();
            var userDtos = result.Should().BeAssignableTo<IEnumerable<UserReadDto>>().Subject;

            // Add more specific assertions based on your use case
            userDtos.Should().HaveCount(2);
            userDtos.Should().ContainSingle(u => u.user_id == 1);
            userDtos.Should().ContainSingle(u => u.user_id == 2);
        }

    }
}
