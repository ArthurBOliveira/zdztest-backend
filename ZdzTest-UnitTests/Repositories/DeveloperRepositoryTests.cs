using Dapper;

using System.Data;

using ZdzTest_Repositories;

public class DeveloperRepositoryTests
{
    private readonly Mock<IDbConnection> _mockDbConnection;
    private readonly DeveloperRepository _repository;

    public DeveloperRepositoryTests()
    {
        _mockDbConnection = new Mock<IDbConnection>();
        _repository = new DeveloperRepository(_mockDbConnection.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnDeveloper_WhenDeveloperExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedDeveloper = new Developer { Id = id, Name = "Test Developer", Role = "Developer", CostPerHour = 50 };
        _mockDbConnection.Setup(db => db.QuerySingleOrDefaultAsync<Developer>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                         .ReturnsAsync(expectedDeveloper);

        // Act
        var result = await _repository.GetByIdAsync(id);

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
        _mockDbConnection.Setup(db => db.QueryAsync<Developer>(It.IsAny<string>(), null, null, null, null))
                         .ReturnsAsync(expectedDevelopers);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(expectedDevelopers, result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddDeveloper()
    {
        // Arrange
        var developer = new Developer { Id = Guid.NewGuid(), Name = "New Developer", Role = "Tester", CostPerHour = 45 };
        _mockDbConnection.Setup(db => db.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                         .ReturnsAsync(1);

        // Act
        var result = await _repository.AddAsync(developer);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateDeveloper()
    {
        // Arrange
        var developer = new Developer { Id = Guid.NewGuid(), Name = "Updated Developer", Role = "Developer", CostPerHour = 55 };
        _mockDbConnection.Setup(db => db.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                         .ReturnsAsync(1);

        // Act
        var result = await _repository.UpdateAsync(developer);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteDeveloper()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockDbConnection.Setup(db => db.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                         .ReturnsAsync(1);

        // Act
        var result = await _repository.DeleteAsync(id);

        // Assert
        Assert.Equal(1, result);
    }
}
