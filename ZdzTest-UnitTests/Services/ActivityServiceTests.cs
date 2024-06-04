using ZdzTest_Repositories;
using ZdzTest_Services;

public class ActivityServiceTests
{
    private readonly Mock<IActivityRepository> _mockRepository;
    private readonly ActivityService _service;

    public ActivityServiceTests()
    {
        _mockRepository = new Mock<IActivityRepository>();
        _service = new ActivityService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnActivity_WhenActivityExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedActivity = new Activity { Id = id, IdDeveloper = Guid.NewGuid(), IdCustomer = Guid.NewGuid(), Hours = 5 };
        _mockRepository.Setup(repo => repo.GetByIdAsync(id))
                       .ReturnsAsync(expectedActivity);

        // Act
        var result = await _service.GetByIdAsync(id);

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
        _mockRepository.Setup(repo => repo.GetAllAsync())
                       .ReturnsAsync(expectedActivities);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(expectedActivities, result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddActivity()
    {
        // Arrange
        var activity = new Activity { Id = Guid.NewGuid(), IdDeveloper = Guid.NewGuid(), IdCustomer = Guid.NewGuid(), Hours = 5 };
        _mockRepository.Setup(repo => repo.AddAsync(activity))
                       .ReturnsAsync(1);

        // Act
        var result = await _service.AddAsync(activity);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateActivity()
    {
        // Arrange
        var activity = new Activity { Id = Guid.NewGuid(), IdDeveloper = Guid.NewGuid(), IdCustomer = Guid.NewGuid(), Hours = 5 };
        _mockRepository.Setup(repo => repo.UpdateAsync(activity))
                       .ReturnsAsync(1);

        // Act
        var result = await _service.UpdateAsync(activity);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteActivity()
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
