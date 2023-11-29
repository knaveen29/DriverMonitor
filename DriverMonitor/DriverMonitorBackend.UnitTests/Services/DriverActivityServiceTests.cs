using DriverMonitorBackend.Models;
using DriverMonitorBackend.Repository;
using DriverMonitorBackend.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverMonitorBackend.UnitTests.Services
{
    public class DriverActivityServiceTests
    {

        [Fact]
        public async Task GetDriversActivity_ShouldReturnDriverActivityResponse()
        {
            // Arrange
            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow;
            var activities = new List<DriverActivityFile>
            {
                new DriverActivityFile { Id = 1, StartTime = DateTime.UtcNow.AddDays(-6), EndTime = DateTime.UtcNow.AddDays(-5), DriverId = 1 },
                new DriverActivityFile { Id = 2, StartTime = DateTime.UtcNow.AddDays(-4), EndTime = DateTime.UtcNow.AddDays(-3), DriverId = 2 },
            };
            var repositoryMock = new Mock<IDriverActivityRepository>();
            repositoryMock.Setup(repo => repo.GetAsync(startDate, endDate)).ReturnsAsync(activities);

            var service = new DriverActivityService(repositoryMock.Object);

            // Act
            var result = await service.GetDriversActivity(startDate, endDate);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Violations);
        }

        [Fact]
        public async Task PostDriverFile_ShouldCallRepositoryPostFileAsync()
        {
            // Arrange
            var file = new DriverActivityFile();
            var repositoryMock = new Mock<IDriverActivityRepository>();
            var service = new DriverActivityService(repositoryMock.Object);

            // Act
            await service.PostDriverFile(file);

            // Assert
            repositoryMock.Verify(repo => repo.PostFileAsync(file), Times.Once);
        }

        [Fact]
        public async Task PostDriverFiles_ShouldCallRepositoryPostFilesAsync()
        {
            // Arrange
            var files = new List<DriverActivityFile> { new DriverActivityFile(), new DriverActivityFile() };
            var repositoryMock = new Mock<IDriverActivityRepository>();
            var service = new DriverActivityService(repositoryMock.Object);

            // Act
            await service.PostDriverFiles(files);

            // Assert
            repositoryMock.Verify(repo => repo.PostFilesAsync(files), Times.Once);
        }

        [Fact]
        public async Task ValidateFiles_ShouldReturnValidationResponse()
        {
            // Arrange
            var creationDate = DateTime.UtcNow;
            var files = new List<DriverActivityFile>
            {
                new DriverActivityFile { Id = 1, StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(1), DriverId = 1 },
                new DriverActivityFile { Id = 2, StartTime = DateTime.UtcNow.AddHours(2), EndTime = DateTime.UtcNow.AddHours(3), DriverId = 2 },
            };
            var existingFiles = new List<DriverActivityFile>
            {
                new DriverActivityFile { Id = 3, StartTime = DateTime.UtcNow.AddDays(-1), EndTime = DateTime.UtcNow.AddDays(-1).AddHours(1), DriverId = 1 },
            };
            var repositoryMock = new Mock<IDriverActivityRepository>();
            repositoryMock.Setup(repo => repo.GetDataByCreationDateAsync(creationDate)).ReturnsAsync(existingFiles);

            var service = new DriverActivityService(repositoryMock.Object);

            // Act
            var result = await service.ValidateFiles(creationDate, files);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsValidSimulation);
        }

    }
}
