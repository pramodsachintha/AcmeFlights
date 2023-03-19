using Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.EventHandlers
{
    public class OrderStatusChangedToConfirmedEventHandler : INotificationHandler<OrderStatusChangedToConfirmedDomainEvent>
    {
        public Task Handle(OrderStatusChangedToConfirmedDomainEvent notification, CancellationToken cancellationToken)
        {
            NotifyCustomers(notification);
            return Task.CompletedTask;
        }

        private bool NotifyCustomers(OrderStatusChangedToConfirmedDomainEvent notification)
        {
            Console.WriteLine($"Order - {notification.Id} is Confirmed. Customers have been Notified!");
            return true;
        }
    }
}
