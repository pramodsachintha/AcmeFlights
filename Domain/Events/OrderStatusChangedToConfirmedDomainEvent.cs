using Domain.Aggregates.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;

namespace Domain.Events
{
    public class OrderStatusChangedToConfirmedDomainEvent : INotification
    {
        public Guid Id { get; private set; }
        public IEnumerable<OrderItem> OrderItems { get; private set; }

        public OrderStatusChangedToConfirmedDomainEvent(Guid id, IEnumerable<OrderItem> orderItems)
        {
            Id = id;
            OrderItems = orderItems;
        }
    }
}
