using API.Application.Dto;
using AutoMapper;
using Domain.Aggregates.OrderAggregate;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class CreateOrderDraftCommandHandler : IRequestHandler<CreateOrderDraftCommand, OrderDraftDto>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderDraftCommandHandler(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        public async Task<OrderDraftDto> Handle(CreateOrderDraftCommand request, CancellationToken cancellationToken)
        {
            var draftOrder = new Order(request.IsRoundTrip, request.TaxRate);
            var orderItems = _mapper.Map<IEnumerable<OrderItemDto>>(request.Items);

            foreach (var item in orderItems)
            {
                draftOrder.AddOrderItem(item.FlightId, item.RateId, item.OriginAirport, item.DestinationAirport, item.UnitPrice, item.Units);
            }

            var order = _orderRepository.Add(draftOrder);

            await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return _mapper.Map<OrderDraftDto>(order);
        }
    }
}
