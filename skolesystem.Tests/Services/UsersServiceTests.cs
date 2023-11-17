using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using skolesystem.DTOs;
using skolesystem.Models;
using skolesystem.Repository;
using skolesystem.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skolesystem.Tests.Services
{
    public class UsersServiceTests
    {
        // Variables
        // Variables
        private readonly UsersService _usersService;
        private readonly Mock<IUsersRepository> _usersRepositoryMock = new Mock<IUsersRepository>();
        private readonly IMapper _mapper; // Add an instance of IMapper

        // Constructor 
        public UsersServiceTests()
        {
            // Initialize IMapper 
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>());
            _mapper = new Mapper(configuration);

            // Provide _mapper to the UsersService constructor
            _usersService = new UsersService(_usersRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAll_ShouldReturnStatusCode200_WithData()
        {
            // Arrange
            var usersData = new List<Users>
            {
                // Add your Users data here
                new Users
                {
                    surname = "Doe",
                    email = "john.doe@example.com",
                    password_hash = "passwordHash",
                    user_id = 1,
                    email_confirmed = 1,
                    lockout_enabled = 1,
                    phone_confirmed = 1,
                    twofactor_enabled = 1,
                    try_failed_count = 1,
                    lockout_end = 1,
                    user_information_id = 1,
                },
            };

            _usersRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(usersData);

            // Act
            var result = await _usersService.GetAllUsers();

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<UserReadDto>>();
            var userDtos = result.Should().BeOfType<List<UserReadDto>>().Subject;

            userDtos.Should().HaveCount(usersData.Count);

        }

        [Fact]
        public async Task GetAll_ShouldReturnStatusCode204_WhenNoDataExists()
        {
            // Arrange
            var emptyUserData = new List<Users>();
            _usersRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(emptyUserData);

            // Act
            var result = await _usersService.GetAllUsers();

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<UserReadDto>>();
            var userDtos = result.Should().BeOfType<List<UserReadDto>>().Subject;

            userDtos.Should().BeEmpty();
        }


        [Fact]
        public async Task GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _usersRepositoryMock.Setup(repo => repo.GetAll()).ThrowsAsync(new Exception("This is an exception"));

            // Act
            Func<Task<IEnumerable<UserReadDto>>> act = async () => await _usersService.GetAllUsers();

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("This is an exception");
        }


        [Fact]
        public async Task GetUserById_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            int userId = 1;
            var user = new Users { /* populate user properties */ };
            _usersRepositoryMock.Setup(repo => repo.GetById(userId)).ReturnsAsync(user);

            // Act
            var result = await _usersService.GetUserById(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UserReadDto>(); // Adjust the type based on your actual return type
        }
        [Fact]
        public async Task GetUserById_ShouldReturnStatusCode404_WhenDataDoesNotExist()
        {
            // Arrange
            int userId = 1;
            _usersRepositoryMock.Setup(repo => repo.GetById(userId)).ReturnsAsync((Users)null);

            // Act
            var result = await _usersService.GetUserById(userId);

            // Assert
            result.Should().BeNull();
        }
        [Fact]
        public async Task GetUserById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int userId = 1;
            _usersRepositoryMock.Setup(repo => repo.GetById(userId)).ThrowsAsync(new Exception("This is an exception"));

            // Act
            Func<Task<UserReadDto>> act = async () => await _usersService.GetUserById(userId);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("This is an exception");
        }

        [Fact]
        public async Task GetDeletedUsers_ShouldReturnStatusCode200_whenDataExists()
        {
            // Arrange
            var deletedUsers = new List<Users> { /* Add some deleted user data */ };
            _usersRepositoryMock.Setup(repo => repo.GetDeletedUsers()).ReturnsAsync(deletedUsers);

            // Act
            var result = await _usersService.GetDeletedUsers();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(deletedUsers.Count);
            
        }

        [Fact]
        public async Task GetDeletedUsers_ShouldReturnStatusCode204_whenNoDataExist()
        {
            // Arrange
            _usersRepositoryMock.Setup(repo => repo.GetDeletedUsers()).ReturnsAsync(new List<Users>());

            // Act
            var result = await _usersService.GetDeletedUsers();

            // Assert
            result.Should().BeEmpty();
        }


        [Fact]
        public async Task GetDeletedUsers_ShouldReturnStatusCode500_whenExceptionIsRaised()
        {
            // Arrange
            _usersRepositoryMock.Setup(repo => repo.GetDeletedUsers()).ThrowsAsync(new Exception("This is an exception"));

            // Act
            Func<Task<IEnumerable<UserReadDto>>> act = async () => await _usersService.GetDeletedUsers();

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("This is an exception");
        }
       
        //Create og Update







    }

}

