using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Domain.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IFlightRepository _flightRepository;

        public ConfirmOrderCommandHandler(IFlightRepository flightRepository, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _flightRepository = flightRepository;
        }

        public async Task<Order> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.OrderId);

            if (order.OrderItems == null || !order.OrderItems.Any())
            {
                throw new OrderingDomainException("At least one flight must be specified.");
            }

            // Check Seat Availability before booking confirmation
            foreach (var item in order.OrderItems)
            {
                var flight = await _flightRepository.GetAsync(item.FlightId);;

                var isAvailable = flight.IsFlightAvailable(item.RateId, item.GetUnits());

                if (!isAvailable)
                {
                    throw new OrderingDomainException($"Flight {item.FlightId} is not available.");
                }
            }

            order.SetOrderConfirmedStatus();
            await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            // If the seats are available mutate the Rate Availability
            foreach (var item in order.OrderItems)
            {
                var flight = await _flightRepository.GetAsync(item.FlightId);
                flight.MutateRateAvailability(item.RateId, item.GetUnits());
            }

            await _flightRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return order;
        }
    }
}
