using Microsoft.AspNetCore.Mvc;
using ZdzTest_Services;
using ZdzTest_API.Controllers;

public class ActivityControllerTests
{
    private readonly Mock<IActivityService> _mockService;
    private readonly ActivityController _controller;

    public ActivityControllerTests()
    {
        _mockService = new Mock<IActivityService>();
        _controller = new ActivityController(_mockService.Object);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenActivityExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedActivity = new Activity { Id = id, IdDeveloper = Guid.NewGuid(), IdCustomer = Guid.NewGuid(), Hours = 5 };
        _mockService.Setup(service => service.GetByIdAsync(id))
                    .ReturnsAsync(expectedActivity);

        // Act
        var result = await _controller.GetById(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Activity>(okResult.Value);
        Assert.Equal(expectedActivity, returnValue);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WithListOfActivities()
    {
        // Arrange
        var expectedActivities = new List<Activity>
        {
            new Activity { Id = Guid.NewGuid(), IdDeveloper = Guid.NewGuid(), IdCustomer = Guid.NewGuid(), Hours = 5 },
            new Activity { Id = Guid.NewGuid(), IdDeveloper = Guid.NewGuid(), IdCustomer = Guid.NewGuid(), Hours = 10 }
        };
        _mockService.Setup(service => service.GetAllAsync())
                    .ReturnsAsync(expectedActivities);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Activity>>(okResult.Value);
        Assert.Equal(expectedActivities, returnValue);
    }

    [Fact]
    public async Task Add_ShouldReturnCreatedAtAction_WhenActivityIsAdded()
    {
        // Arrange
        var activity = new Activity { Id = Guid.NewGuid(), IdDeveloper = Guid.NewGuid(), IdCustomer = Guid.NewGuid(), Hours = 5 };
        _mockService.Setup(service => service.AddAsync(activity))
                    .ReturnsAsync(1);

        // Act
        var result = await _controller.Add(activity);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Activity>(createdAtActionResult.Value);
        Assert.Equal(activity, returnValue);
        Assert.Equal(nameof(_controller.GetById), createdAtActionResult.ActionName);
        Assert.Equal(activity.Id, createdAtActionResult.RouteValues["id"]);
    }

    [Fact]
    public async Task Update_ShouldReturnNoContent_WhenActivityIsUpdated()
    {
        // Arrange
        var id = Guid.NewGuid();
        var activity = new Activity { Id = id, IdDeveloper = Guid.NewGuid(), IdCustomer = Guid.NewGuid(), Hours = 5 };
        _mockService.Setup(service => service.UpdateAsync(activity))
                    .ReturnsAsync(1);

        // Act
        var result = await _controller.Update(id, activity);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenActivityIsDeleted()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockService.Setup(service => service.DeleteAsync(id))
                    .ReturnsAsync(1);

        // Act
        var result = await _controller.Delete(id);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
