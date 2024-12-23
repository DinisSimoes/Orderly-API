using Moq;
using Orderly.Application.Services.Order;
using Orderly.Domain.Entities;
using Orderly.Domain.Interfaces.Repositories.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Tests.Services.Orders
{
    public class OrderQueryServiceTests
    {
        private readonly Mock<IOrderQueryRepository> _mockOrderQueryRepository;
        private readonly OrderQueryService _orderQueryService;

        public OrderQueryServiceTests()
        {
            _mockOrderQueryRepository = new Mock<IOrderQueryRepository>();
            _orderQueryService = new OrderQueryService(_mockOrderQueryRepository.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = Guid.Parse("bf4fbffb-06d9-413b-9791-2aa1c3c39ccf");
            var order = new Order { Id = Guid.Parse("bf4fbffb-06d9-413b-9791-2aa1c3c39ccf"), CustomerId = Guid.NewGuid(), OrderDate = DateTime.Now, Status = 0, TotalAmount = 100 };

            _mockOrderQueryRepository.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(order);

            // Act
            var result = await _orderQueryService.GetByIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.Id);
            Assert.Equal(100, result.TotalAmount);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            _mockOrderQueryRepository.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync((Order)null);

            // Act
            var result = await _orderQueryService.GetByIdAsync(orderId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), OrderDate = DateTime.Now, Status = 0, TotalAmount = 100 },
                new Order { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), OrderDate = DateTime.Now, Status = 0, TotalAmount = 200 }
            };

            _mockOrderQueryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orders);

            // Act
            var result = await _orderQueryService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByCustomerIdAsync_ShouldReturnOrders_WhenOrdersExistForCustomer()
        {
            // Arrange
            var customerId = Guid.Parse("bf4fbffb-06d9-413b-9791-2aa1c3c39ccf");
            var orders = new List<Order>
            {
                new Order { Id = Guid.NewGuid(), CustomerId = Guid.Parse("bf4fbffb-06d9-413b-9791-2aa1c3c39ccf"), OrderDate = DateTime.Now, Status = 0, TotalAmount = 200 }
            };

            _mockOrderQueryRepository.Setup(repo => repo.GetByCustomerIdAsync(customerId)).ReturnsAsync(orders);

            // Act
            var result = await _orderQueryService.GetByCustomerIdAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
            Assert.All(result, order => Assert.Equal(customerId, order.CustomerId));
        }

        [Fact]
        public async Task GetByCustomerIdAsync_ShouldReturnEmpty_WhenNoOrdersExistForCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            _mockOrderQueryRepository.Setup(repo => repo.GetByCustomerIdAsync(customerId)).ReturnsAsync(new List<Order>());

            // Act
            var result = await _orderQueryService.GetByCustomerIdAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Theory]
        [InlineData("Customer 1", 2)]
        [InlineData("Customer 2", 0)]
        public async Task GetByCustomerIdAsync_ShouldReturnCorrectNumberOfOrders_ForDifferentCustomers(string customerName, int expectedOrderCount)
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var orders = new List<Order>();

            for (int i = 0; i < expectedOrderCount; i++)
            {
                orders.Add(new Order { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), OrderDate = DateTime.Now, Status = 0, TotalAmount = 100 });
            }

            _mockOrderQueryRepository.Setup(repo => repo.GetByCustomerIdAsync(customerId)).ReturnsAsync(orders);

            // Act
            var result = await _orderQueryService.GetByCustomerIdAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedOrderCount, result.Count());
        }
    }
}
