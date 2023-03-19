using Domain.Aggregates.OrderAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class UpdateOrderItemsCommandHandler : IRequestHandler<UpdateOrderItemsCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        public UpdateOrderItemsCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order> Handle(UpdateOrderItemsCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.OrderId);

            foreach (var item in request.OrderItems)
            {
                order.AddOrderItem(item.FlightId, item.RateId, item.OriginAirport, item.DestinationAirport, item.UnitPrice, item.Units);
            }

            _orderRepository.Update(order);

            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return order;
        }
    }
}
