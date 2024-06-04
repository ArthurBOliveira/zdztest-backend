using Microsoft.AspNetCore.Mvc;
using ZdzTest_API.Controllers;
using ZdzTest_Services;

public class DeveloperControllerTests
{
    private readonly Mock<IDeveloperService> _mockService;
    private readonly DeveloperController _controller;

    public DeveloperControllerTests()
    {
        _mockService = new Mock<IDeveloperService>();
        _controller = new DeveloperController(_mockService.Object);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenDeveloperExists()
    {
        var id = Guid.NewGuid();
        var expectedDeveloper = new Developer { Id = id, Name = "Test Developer", Role = "Developer", CostPerHour = 50 };
        _mockService.Setup(service => service.GetByIdAsync(id))
                    .ReturnsAsync(expectedDeveloper);

        var result = await _controller.GetById(id);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Developer>(okResult.Value);
        Assert.Equal(expectedDeveloper, returnValue);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WithListOfDevelopers()
    {
        var expectedDevelopers = new List<Developer>
        {
            new Developer { Id = Guid.NewGuid(), Name = "Developer 1", Role = "Developer", CostPerHour = 50 },
            new Developer { Id = Guid.NewGuid(), Name = "Developer 2", Role = "Tester", CostPerHour = 40 }
        };
        _mockService.Setup(service => service.GetAllAsync())
                    .ReturnsAsync(expectedDevelopers);

        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Developer>>(okResult.Value);
        Assert.Equal(expectedDevelopers, returnValue);
    }

    [Fact]
    public async Task Add_ShouldReturnCreatedAtAction_WhenDeveloperIsAdded()
    {
        var developer = new Developer { Id = Guid.NewGuid(), Name = "New Developer", Role = "Tester", CostPerHour = 45 };
        _mockService.Setup(service => service.AddAsync(developer))
                    .ReturnsAsync(1);

        var result = await _controller.Add(developer);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Developer>(createdAtActionResult.Value);
        Assert.Equal(developer, returnValue);
        Assert.Equal(nameof(_controller.GetById), createdAtActionResult.ActionName);
        Assert.Equal(developer.Id, createdAtActionResult.RouteValues["id"]);
    }

    [Fact]
    public async Task Update_ShouldReturnNoContent_WhenDeveloperIsUpdated()
    {
        var id = Guid.NewGuid();
        var developer = new Developer { Id = id, Name = "Updated Developer", Role = "Developer", CostPerHour = 55 };
        _mockService.Setup(service => service.UpdateAsync(developer))
                    .ReturnsAsync(1);

        var result = await _controller.Update(id, developer);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenDeveloperIsDeleted()
    {
        var id = Guid.NewGuid();
        _mockService.Setup(service => service.DeleteAsync(id))
                    .ReturnsAsync(1);

        var result = await _controller.Delete(id);

        Assert.IsType<NoContentResult>(result);
    }
}