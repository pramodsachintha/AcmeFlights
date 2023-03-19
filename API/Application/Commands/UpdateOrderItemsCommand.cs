using Domain.Aggregates.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;

namespace API.Application.Commands
{
    public class UpdateOrderItemsCommand : IRequest<Order>
    {
        public Guid OrderId { get; set; }

        public IEnumerable<BookingItem> OrderItems { get; set; }

        public UpdateOrderItemsCommand(Guid orderId, IEnumerable<BookingItem> items)
        {
            OrderId = orderId;
            OrderItems = items;
        }
    }
}
