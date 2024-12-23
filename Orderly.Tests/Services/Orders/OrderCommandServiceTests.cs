using Moq;
using Orderly.Application.Services.Order;
using Orderly.Domain.Entities;
using Orderly.Domain.Interfaces.Messaging.Kafka;
using Orderly.Domain.Interfaces.Repositories.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orderly.Tests.Services.Orders
{
    public class OrderCommandServiceTests
    {
        private readonly Mock<IOrderCommandRepository> _mockOrderCommandRepository;
        private readonly Mock<IOrderQueryRepository> _mockOrderQueryRepository;
        private readonly Mock<IOrderEventProducer> _mockOrderEventProducer;
        private readonly OrderCommandService _orderCommandService;

        public OrderCommandServiceTests()
        {
            _mockOrderCommandRepository = new Mock<IOrderCommandRepository>();
            _mockOrderQueryRepository = new Mock<IOrderQueryRepository>();
            _mockOrderEventProducer = new Mock<IOrderEventProducer>();

            _orderCommandService = new OrderCommandService(
                _mockOrderCommandRepository.Object,
                _mockOrderQueryRepository.Object,
                _mockOrderEventProducer.Object
            );
        }

        [Fact]
        public async Task AddAsync_ShouldCallAddAndPublishEvent()
        {
            // Arrange
            var order = new Order { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), OrderDate = DateTime.Now, Status = 0, TotalAmount = 100 };

            // Act
            await _orderCommandService.AddAsync(order);

            // Assert
            _mockOrderCommandRepository.Verify(repo => repo.AddAsync(It.Is<Order>(o => o == order)), Times.Once);
            _mockOrderEventProducer.Verify(producer => producer.PublishOrderCreatedEvent("Create", It.Is<Order>(o => o == order)), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldCallUpdateAndPublishEvent()
        {
            // Arrange
            var order = new Order { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), OrderDate = DateTime.Now, Status = 0, TotalAmount = 100 };

            // Act
            await _orderCommandService.UpdateAsync(order);

            // Assert
            _mockOrderCommandRepository.Verify(repo => repo.UpdateAsync(It.Is<Order>(o => o == order)), Times.Once);
            _mockOrderEventProducer.Verify(producer => producer.PublishOrderCreatedEvent("Update", It.Is<Order>(o => o == order)), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallDeleteAndPublishEvent_WhenOrderExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), OrderDate = DateTime.Now, Status = 0, TotalAmount = 100 };

            _mockOrderQueryRepository.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(order);

            // Act
            await _orderCommandService.DeleteAsync(orderId);

            // Assert
            _mockOrderCommandRepository.Verify(repo => repo.DeleteAsync(It.Is<Guid>(id => id == orderId)), Times.Once);
            _mockOrderEventProducer.Verify(producer => producer.PublishOrderCreatedEvent("Delete", It.Is<Order>(o => o == order)), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenOrderNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            _mockOrderQueryRepository.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync((Order)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _orderCommandService.DeleteAsync(orderId));
            Assert.Equal($"Order with ID {orderId} not found.", exception.Message);
        }

        [Theory]
        [InlineData("Create")]
        [InlineData("Update")]
        [InlineData("Delete")]
        public async Task AddUpdateDeleteAsync_ShouldPublishCorrectEvent(string eventType)
        {
            // Arrange
            var order = new Order { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), OrderDate = DateTime.Now, Status = 0, TotalAmount = 100 };

            if (eventType == "Create")
            {
                // Act
                await _orderCommandService.AddAsync(order);

                // Assert
                _mockOrderEventProducer.Verify(producer => producer.PublishOrderCreatedEvent("Create", It.Is<Order>(o => o == order)), Times.Once);
            }
            else if (eventType == "Update")
            {
                // Act
                await _orderCommandService.UpdateAsync(order);

                // Assert
                _mockOrderEventProducer.Verify(producer => producer.PublishOrderCreatedEvent("Update", It.Is<Order>(o => o == order)), Times.Once);
            }
            else if (eventType == "Delete")
            {
                // Arrange
                var orderId = Guid.NewGuid();
                _mockOrderQueryRepository.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(order);

                // Act
                await _orderCommandService.DeleteAsync(orderId);

                // Assert
                _mockOrderEventProducer.Verify(producer => producer.PublishOrderCreatedEvent("Delete", It.Is<Order>(o => o == order)), Times.Once);
            }
        }
    }
}
