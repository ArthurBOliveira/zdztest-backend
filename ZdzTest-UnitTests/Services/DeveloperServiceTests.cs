using ZdzTest_Repositories;
using ZdzTest_Services;

public class DeveloperServiceTests
{
    private readonly Mock<IDeveloperRepository> _mockRepository;
    private readonly DeveloperService _service;

    public DeveloperServiceTests()
    {
        _mockRepository = new Mock<IDeveloperRepository>();
        _service = new DeveloperService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnDeveloper_WhenDeveloperExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedDeveloper = new Developer { Id = id, Name = "Test Developer", Role = "Developer", CostPerHour = 50 };
        _mockRepository.Setup(repo => repo.GetByIdAsync(id))
                       .ReturnsAsync(expectedDeveloper);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        Assert.Equal(expectedDeveloper, result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllDevelopers()
    {
        // Arrange
        var expectedDevelopers = new List<Developer>
        {
            new Developer { Id = Guid.NewGuid(), Name = "Developer 1", Role = "Developer", CostPerHour = 50 },
            new Developer { Id = Guid.NewGuid(), Name = "Developer 2", Role = "Tester", CostPerHour = 40 }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync())
                       .ReturnsAsync(expectedDevelopers);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(expectedDevelopers, result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddDeveloper()
    {
        // Arrange
        var developer = new Developer { Id = Guid.NewGuid(), Name = "New Developer", Role = "Tester", CostPerHour = 45 };
        _mockRepository.Setup(repo => repo.AddAsync(developer))
                       .ReturnsAsync(1);

        // Act
        var result = await _service.AddAsync(developer);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateDeveloper()
    {
        // Arrange
        var developer = new Developer { Id = Guid.NewGuid(), Name = "Updated Developer", Role = "Developer", CostPerHour = 55 };
        _mockRepository.Setup(repo => repo.UpdateAsync(developer))
                       .ReturnsAsync(1);

        // Act
        var result = await _service.UpdateAsync(developer);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteDeveloper()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockRepository.Setup(repo => repo.DeleteAsync(id))
                       .ReturnsAsync(1);

        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        Assert.Equal(1, result);
    }
}
