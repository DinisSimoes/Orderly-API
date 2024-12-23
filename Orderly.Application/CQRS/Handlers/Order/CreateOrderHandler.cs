using MediatR;
using Orderly.Application.CQRS.Commands.Order;
using Orderly.Domain.Entities;
using Orderly.Domain.Interfaces.Repositories.Product;
using Orderly.Domain.Interfaces.Services.Order;

namespace Orderly.Application.CQRS.Handlers.Order
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderCommandService _orderCommandService;
        private readonly IProductQueryRepository _productQueryRepository;

        public CreateOrderHandler(IOrderCommandService orderCommandService, IProductQueryRepository productQueryRepository)
        {
            _orderCommandService = orderCommandService;
            _productQueryRepository = productQueryRepository;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Guid orderId = Guid.NewGuid();
            var orderItemsWithProductIds = new List<OrderItem>();
            var total = 0m;

            foreach (var oi in request.OrderItems)
            {
                var product = await _productQueryRepository.GetByNameAsync(oi.ProductName);

                if (product == null)
                {
                    throw new Exception($"Product with name {oi.ProductName} not found.");
                }

                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductId = product.Id,
                    ProductName = oi.ProductName,
                    Quantity = oi.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = product.Price * oi.Quantity
                };

                orderItemsWithProductIds.Add(orderItem);
                total = total + orderItem.TotalPrice;
            }

            var order = new Domain.Entities.Order
            {
                Id = orderId,
                CustomerId = Guid.Parse("123e4567-e89b-12d3-a456-426614174000"),
                OrderDate = DateTime.Now,
                TotalAmount = total,
                OrderItems = orderItemsWithProductIds
            };

            await _orderCommandService.AddAsync(order);

            return order.Id;
        }
    }
}
