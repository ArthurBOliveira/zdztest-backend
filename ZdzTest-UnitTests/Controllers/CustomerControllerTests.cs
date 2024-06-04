using Microsoft.AspNetCore.Mvc;
using ZdzTest_API.Controllers;
using ZdzTest_Services;

public class CustomerControllerTests
{
    private readonly Mock<ICustomerService> _mockService;
    private readonly CustomerController _controller;

    public CustomerControllerTests()
    {
        _mockService = new Mock<ICustomerService>();
        _controller = new CustomerController(_mockService.Object);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenCustomerExists()
    {
        var id = Guid.NewGuid();
        var expectedCustomer = new Customer { Id = id, Name = "Test Customer", MonthlyBudget = 1000 };
        _mockService.Setup(service => service.GetByIdAsync(id))
                    .ReturnsAsync(expectedCustomer);

        var result = await _controller.GetById(id);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Customer>(okResult.Value);
        Assert.Equal(expectedCustomer, returnValue);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WithListOfCustomers()
    {
        var expectedCustomers = new List<Customer>
        {
            new Customer { Id = Guid.NewGuid(), Name = "Customer 1", MonthlyBudget = 1000 },
            new Customer { Id = Guid.NewGuid(), Name = "Customer 2", MonthlyBudget = 2000 }
        };
        _mockService.Setup(service => service.GetAllAsync())
                    .ReturnsAsync(expectedCustomers);

        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<Customer>>(okResult.Value);
        Assert.Equal(expectedCustomers, returnValue);
    }

    [Fact]
    public async Task Add_ShouldReturnCreatedAtAction_WhenCustomerIsAdded()
    {
        var customer = new Customer { Id = Guid.NewGuid(), Name = "New Customer", MonthlyBudget = 1500 };
        _mockService.Setup(service => service.AddAsync(customer))
                    .ReturnsAsync(1);

        var result = await _controller.Add(customer);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Customer>(createdAtActionResult.Value);
        Assert.Equal(customer, returnValue);
        Assert.Equal(nameof(_controller.GetById), createdAtActionResult.ActionName);
        Assert.Equal(customer.Id, createdAtActionResult.RouteValues["id"]);
    }

    [Fact]
    public async Task Update_ShouldReturnNoContent_WhenCustomerIsUpdated()
    {
        var id = Guid.NewGuid();
        var customer = new Customer { Id = id, Name = "Updated Customer", MonthlyBudget = 1500 };
        _mockService.Setup(service => service.UpdateAsync(customer))
                    .ReturnsAsync(1);

        var result = await _controller.Update(id, customer);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenCustomerIsDeleted()
    {
        var id = Guid.NewGuid();
        _mockService.Setup(service => service.DeleteAsync(id))
                    .ReturnsAsync(1);

        var result = await _controller.Delete(id);

        Assert.IsType<NoContentResult>(result);
    }
}
