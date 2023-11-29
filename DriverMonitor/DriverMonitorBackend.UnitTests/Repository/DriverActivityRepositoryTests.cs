using DriverMonitorBackend.Models;
using DriverMonitorBackend.Repository;
using Microsoft.EntityFrameworkCore;

namespace DriverMonitorBackend.UnitTests.Repository
{
    public class DriverActivityRepositoryTests
    {
        private readonly DbContextOptions<ApiDBContext> _options;
        private readonly ApiDBContext _testDbContext;

        public DriverActivityRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApiDBContext>()
                .UseInMemoryDatabase(databaseName: "driver_monitor")
                .Options;

            _testDbContext = new ApiDBContext(_options);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnCorrectData()
        {
            // Arrange
            var repository = new DriverActivityRepository(_testDbContext);
            var startDate = new DateTime(2023, 11, 1);
            var endDate = new DateTime(2023, 11, 5);

            var testData = new List<DriverActivityFile>
            {
                new DriverActivityFile { DriverId = 1, CreateTime = new DateTime(2023, 11, 2), Activity = "Drive" },
                new DriverActivityFile { DriverId = 2,  CreateTime = new DateTime(2023, 11, 3), Activity = "Rest"  },
                new DriverActivityFile { DriverId = 3, CreateTime = new DateTime(2023, 11, 6), Activity = "Drive"  }, // This should not be included
            };

            await _testDbContext.DriverActivityFiles.AddRangeAsync(testData);
            await _testDbContext.SaveChangesAsync();

            // Act
            var result = await repository.GetAsync(startDate, endDate);

            // Assert
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public async Task GetDataByCreationDateAsync_ShouldReturnCorrectData()
        {
            // Arrange
            var repository = new DriverActivityRepository(_testDbContext);
            var creationDate = new DateTime(2023, 11, 3);
            var testData = new List<DriverActivityFile>
            {
                new DriverActivityFile { DriverId = 2,  CreateTime = new DateTime(2023, 11, 3), Activity = "Rest"  },
                new DriverActivityFile { DriverId = 3, CreateTime = new DateTime(2023, 11, 6), Activity = "Drive"  }, // This should not be included
            };
            await _testDbContext.DriverActivityFiles.AddRangeAsync(testData);
            await _testDbContext.SaveChangesAsync();

            // Act
            var result = await repository.GetDataByCreationDateAsync(creationDate);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task PostFileAsync_ShouldAddFileToDatabase()
        {
            // Arrange
            var repository = new DriverActivityRepository(_testDbContext);
            var file = new DriverActivityFile { CreateTime = new DateTime(2023, 11, 01), Activity = "Drive" };

            // Act
            await repository.PostFileAsync(file);

            // Assert
            var result = await _testDbContext.DriverActivityFiles.FindAsync(file.Id);
            Assert.NotNull(result);
            Assert.Equal(file.CreateTime, result.CreateTime);
        }

        [Fact]
        public async Task PostFilesAsync_ShouldAddFilesToDatabase()
        {
            // Arrange
            var repository = new DriverActivityRepository(_testDbContext);
            var files = new List<DriverActivityFile>
            {
                new DriverActivityFile { DriverId = 1, CreateTime = new DateTime(2023, 11, 2), Activity = "Drive" },
                new DriverActivityFile { DriverId = 2, CreateTime = new DateTime(2023, 11, 3), Activity = "Rest" },
            };

            // Act
            await repository.PostFilesAsync(files);

            // Assert
            var result = await _testDbContext.DriverActivityFiles.ToListAsync();
            Assert.Equal(2, result.Count);
            Assert.Contains(result, file => file.CreateTime == new DateTime(2023, 11, 2));
            Assert.Contains(result, file => file.CreateTime == new DateTime(2023, 11, 3));
        }

        public void Dispose()
        {
            _testDbContext.Dispose();
        }
    }
}
