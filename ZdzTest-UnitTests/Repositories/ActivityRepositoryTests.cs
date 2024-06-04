using Dapper;

using System.Data;

using ZdzTest_Repositories;

public class ActivityRepositoryTests
{
    private readonly Mock<IDbConnection> _mockDbConnection;
    private readonly ActivityRepository _repository;

    public ActivityRepositoryTests()
    {
        _mockDbConnection = new Mock<IDbConnection>();
        _repository = new ActivityRepository(_mockDbConnection.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnActivity_WhenActivityExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedActivity = new Activity { Id = id, IdDeveloper = Guid.NewGuid(), IdCustomer = Guid.NewGuid(), Hours = 5 };
        _mockDbConnection.Setup(db => db.QuerySingleOrDefaultAsync<Activity>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                         .ReturnsAsync(expectedActivity);

        // Act
        var result = await _repository.GetByIdAsync(id);

        // Assert
        Assert.Equal(expectedActivity, result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllActivities()
    {
        // Arrange
        var expectedActivities = new List<Activity>
            {
                new Activity { Id = Guid.NewGuid(), IdDeveloper = Guid.NewGuid(), IdCustomer = Guid.NewGuid(), Hours = 5 },
                new Activity { Id = Guid.NewGuid(), IdDeveloper = Guid.NewGuid(), IdCustomer = Guid.NewGuid(), Hours = 10 }
            };
        _mockDbConnection.Setup(db => db.QueryAsync<Activity>(It.IsAny<string>(), null, null, null, null))
                         .ReturnsAsync(expectedActivities);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(expectedActivities, result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddActivity()
    {
        // Arrange
        var activity = new Activity { Id = Guid.NewGuid(), IdDeveloper = Guid.NewGuid(), IdCustomer = Guid.NewGuid(), Hours = 5 };
        _mockDbConnection.Setup(db => db.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                         .ReturnsAsync(1);

        // Act
        var result = await _repository.AddAsync(activity);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateActivity()
    {
        // Arrange
        var activity = new Activity { Id = Guid.NewGuid(), IdDeveloper = Guid.NewGuid(), IdCustomer = Guid.NewGuid(), Hours = 5 };
        _mockDbConnection.Setup(db => db.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                         .ReturnsAsync(1);

        // Act
        var result = await _repository.UpdateAsync(activity);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteActivity()
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