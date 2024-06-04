using Moq;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

using ZdzTest_Repositories;
using ZdzTest_Services;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _mockRepository;
    private readonly CustomerService _service;

    public CustomerServiceTests()
    {
        _mockRepository = new Mock<ICustomerRepository>();
        _service = new CustomerService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCustomer_WhenCustomerExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedCustomer = new Customer { Id = id, Name = "Test Customer", MonthlyBudget = 1000 };
        _mockRepository.Setup(repo => repo.GetByIdAsync(id))
                       .ReturnsAsync(expectedCustomer);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        Assert.Equal(expectedCustomer, result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCustomers()
    {
        // Arrange
        var expectedCustomers = new List<Customer>
        {
            new Customer { Id = Guid.NewGuid(), Name = "Customer 1", MonthlyBudget = 1000 },
            new Customer { Id = Guid.NewGuid(), Name = "Customer 2", MonthlyBudget = 2000 }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync())
                       .ReturnsAsync(expectedCustomers);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.Equal(expectedCustomers, result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddCustomer()
    {
        // Arrange
        var customer = new Customer { Id = Guid.NewGuid(), Name = "New Customer", MonthlyBudget = 1500 };
        _mockRepository.Setup(repo => repo.AddAsync(customer))
                       .ReturnsAsync(1);

        // Act
        var result = await _service.AddAsync(customer);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCustomer()
    {
        // Arrange
        var customer = new Customer { Id = Guid.NewGuid(), Name = "Updated Customer", MonthlyBudget = 1500 };
        _mockRepository.Setup(repo => repo.UpdateAsync(customer))
                       .ReturnsAsync(1);

        // Act
        var result = await _service.UpdateAsync(customer);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteCustomer()
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
