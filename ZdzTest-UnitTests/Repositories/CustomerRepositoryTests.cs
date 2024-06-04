using Dapper;

using System.Data;

using ZdzTest_Repositories;

public class CustomerRepositoryTests
{
    private readonly Mock<IDbConnection> _mockDbConnection;
    private readonly CustomerRepository _repository;

    public CustomerRepositoryTests()
    {
        _mockDbConnection = new Mock<IDbConnection>();
        _repository = new CustomerRepository(_mockDbConnection.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCustomer_WhenCustomerExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var expectedCustomer = new Customer { Id = id, Name = "Test Customer", MonthlyBudget = 1000 };
        _mockDbConnection.Setup(db => db.QuerySingleOrDefaultAsync<Customer>(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                         .ReturnsAsync(expectedCustomer);

        // Act
        var result = await _repository.GetByIdAsync(id);

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
        _mockDbConnection.Setup(db => db.QueryAsync<Customer>(It.IsAny<string>(), null, null, null, null))
                         .ReturnsAsync(expectedCustomers);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(expectedCustomers, result);
    }

    [Fact]
    public async Task AddAsync_ShouldAddCustomer()
    {
        // Arrange
        var customer = new Customer { Id = Guid.NewGuid(), Name = "New Customer", MonthlyBudget = 1500 };
        _mockDbConnection.Setup(db => db.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                         .ReturnsAsync(1);

        // Act
        var result = await _repository.AddAsync(customer);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCustomer()
    {
        // Arrange
        var customer = new Customer { Id = Guid.NewGuid(), Name = "Updated Customer", MonthlyBudget = 1500 };
        _mockDbConnection.Setup(db => db.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), null, null, null))
                         .ReturnsAsync(1);

        // Act
        var result = await _repository.UpdateAsync(customer);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteCustomer()
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
