using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using skolesystem.Controllers;
using skolesystem.DTOs;
using skolesystem.Models;
using skolesystem.Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace skolesystem.Tests.Controller
{
    public class BrugerControllerTests
    {
        private readonly Mock<IBrugerService> _brugerServiceMock;
        private readonly BrugerController _brugerController;

        public BrugerControllerTests()
        {
            _brugerServiceMock = new Mock<IBrugerService>();
            _brugerController = new BrugerController(_brugerServiceMock.Object);
        }

        [Fact]
        public async Task GetBrugers_ShouldReturnBrugerReadDtos_WhenBrugersExist()
        {
            // Arrange
            var brugers = new List<Bruger>
            {
                new Bruger { user_information_id = 1, name = "John", last_name = "Doe", phone = "123456", date_of_birth = "1995", address = "123 Main St", is_deleted = false, gender_id = 1, city_id = 1, user_id = 1 },
                new Bruger { user_information_id = 2, name = "Jane", last_name = "Doe", phone = "654321", date_of_birth = "1995", address = "456 Oak St", is_deleted = false, gender_id = 2, city_id = 2, user_id = 2 }
            };

            _brugerServiceMock.Setup(repo => repo.GetAllBrugers()).ReturnsAsync(brugers);

            // Act
            var result = await _brugerController.GetBrugers();

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<BrugerReadDto>>();
            var brugerDtos = (IEnumerable<BrugerReadDto>)result;
            brugerDtos.Should().NotBeNullOrEmpty().And.HaveCount(2);
        }

        [Fact]
        public async Task GetBrugers_ShouldReturnEmptyList_WhenNoBrugersExist()
        {
            // Arrange
            _brugerServiceMock.Setup(repo => repo.GetAllBrugers()).ReturnsAsync(new List<Bruger>());

            // Act
            var result = await _brugerController.GetBrugers();

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<IEnumerable<BrugerReadDto>>();
            var brugerDtos = (IEnumerable<BrugerReadDto>)result;
            brugerDtos.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBrugerById_ShouldReturnBrugerReadDto_WhenBrugerExists()
        {
            // Arrange
            int brugerId = 1;
            var existingBruger = new Bruger { user_information_id = brugerId, name = "John", last_name = "Doe", phone = "123456", date_of_birth = "1998-01-01", address = "123 Main St", is_deleted = false, gender_id = 1, city_id = 1, user_id = 1 };

            _brugerServiceMock.Setup(repo => repo.GetBrugerById(brugerId)).ReturnsAsync(existingBruger);

            // Act
            var result = await _brugerController.GetBrugerById(brugerId);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<OkObjectResult>();
            var okObjectResult = (OkObjectResult)result;
            okObjectResult.Value.Should().NotBeNull().And.BeAssignableTo<BrugerReadDto>();
            var brugerDto = (BrugerReadDto)okObjectResult.Value;
            brugerDto.user_information_id.Should().Be(existingBruger.user_information_id);
        }

        [Fact]
        public async Task GetBrugerById_ShouldReturnNotFound_WhenBrugerDoesNotExist()
        {
            // Arrange
            int brugerId = 1;

            _brugerServiceMock.Setup(repo => repo.GetBrugerById(brugerId)).ReturnsAsync((Bruger)null);

            // Act
            var result = await _brugerController.GetBrugerById(brugerId);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<NotFoundResult>();
        }

        [Fact]
        public async Task CreateBruger_ShouldReturnCreatedAtAction_WhenBrugerIsCreated()
        {
            // Arrange
            var brugerDto = new BrugerCreateDto
            {
                name = "John",
                last_name = "Doe",
                phone = "123456",
                date_of_birth = "1998-01-01",
                address = "123 Main St",
                is_deleted = false,
                gender_id = 1,
                city_id = 1,
                user_id = 1
            };

            _brugerServiceMock.Setup(repo => repo.AddBruger(It.IsAny<Bruger>())).Returns(Task.CompletedTask);

            // Act
            var result = await _brugerController.CreateBruger(brugerDto);

            // Assert
            result.Should().NotBeNull().And.BeAssignableTo<CreatedAtActionResult>();
            var createdAtActionResult = (CreatedAtActionResult)result;
            createdAtActionResult.Value.Should().BeEquivalentTo(brugerDto);

            createdAtActionResult.ActionName.Should().Be(nameof(_brugerController.GetBrugerById));
            createdAtActionResult.RouteValues.Should().ContainKey("id");
            createdAtActionResult.RouteValues["id"].Should().NotBeNull();
        }

        [Fact]
        public async Task CreateBruger_ShouldReturnConflict_WhenBrugerAlreadyExists()
        {
            // Arrange
            var brugerDto = new BrugerCreateDto
            {
                name = "John",
                last_name = "Doe",
                phone = "123456",
                date_of_birth = "1998-01-01",
                address = "123 Main St",
                is_deleted = false,
                gender_id = 1,
                city_id = 1,
                user_id = 1
            };

            _brugerServiceMock.Setup(repo => repo.AddBruger(It.IsAny<Bruger>())).ThrowsAsync(new ArgumentException("User with the specified ID already exists"));

            // Act
            var result = await _brugerController.CreateBruger(brugerDto);

            // Assert
            result.Should().BeAssignableTo<ConflictObjectResult>().And.Match<ConflictObjectResult>(r => r.Value.Equals("User with the specified ID already exists"));
        }
        [Fact]
        public async Task UpdateBruger_ShouldReturnNoContent_WhenBrugerExists()
        {
            // Arrange
            int brugerId = 1;
            var updatedBrugerDto = new BrugerUpdateDto
            {
                name = "Updated Name",
                last_name = "Updated Last Name",
                phone = "987654",
                date_of_birth = "1990-02-15",
                address = "456 Oak St",
                is_deleted = true
               
            };

            var existingBruger = new Bruger
            {
                user_information_id = brugerId,
                name = "John",
                last_name = "Doe",
                phone = "123456",
                date_of_birth = "1998-01-01",
                address = "123 Main St",
                is_deleted = false

            };

            _brugerServiceMock.Setup(repo => repo.GetBrugerById(brugerId)).ReturnsAsync(existingBruger);

            // Act
            var result = await _brugerController.UpdateBruger(brugerId, updatedBrugerDto);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _brugerServiceMock.Verify(repo => repo.UpdateBruger(It.IsAny<Bruger>()), Times.Once);
        }

        [Fact]
        public async Task UpdateBruger_ShouldReturnNotFound_WhenBrugerDoesNotExist()
        {
            // Arrange
            int brugerId = 1;
            var updatedBrugerDto = new BrugerUpdateDto
            {
                name = "Updated Name",
                last_name = "Updated Last Name",
                phone = "987654",
                date_of_birth = "1990-02-15",
                address = "456 Oak St",
                is_deleted = true,
 
            };

            _brugerServiceMock.Setup(repo => repo.GetBrugerById(brugerId)).ReturnsAsync((Bruger)null);

            // Act
            var result = await _brugerController.UpdateBruger(brugerId, updatedBrugerDto);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            _brugerServiceMock.Verify(repo => repo.UpdateBruger(It.IsAny<Bruger>()), Times.Never);
        }
        [Fact]
        public async Task DeleteBruger_ShouldReturnNoContent_WhenBrugerExists()
        {
            // Arrange
            int brugerId = 1;
            var existingBruger = new Bruger
            {
                user_information_id = brugerId,
                name = "John",
                last_name = "Doe",
                phone = "123456",
                date_of_birth = "1998-01-01",
                address = "123 Main St",
                is_deleted = false,
                gender_id = 1,
                city_id = 1,
                user_id = 1
            };

            _brugerServiceMock.Setup(repo => repo.GetBrugerById(brugerId)).ReturnsAsync(existingBruger);

            // Act
            var result = await _brugerController.DeleteBruger(brugerId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _brugerServiceMock.Verify(repo => repo.SoftDeleteBruger(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeleteBruger_ShouldReturnNotFound_WhenBrugerDoesNotExist()
        {
            // Arrange
            int brugerId = 1;

            _brugerServiceMock.Setup(repo => repo.GetBrugerById(brugerId)).ReturnsAsync((Bruger)null);

            // Act
            var result = await _brugerController.DeleteBruger(brugerId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            _brugerServiceMock.Verify(repo => repo.SoftDeleteBruger(It.IsAny<int>()), Times.Never);
        }

    }
}
