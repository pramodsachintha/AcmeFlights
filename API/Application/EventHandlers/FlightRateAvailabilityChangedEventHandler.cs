using Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.EventHandlers
{
    public class FlightRateAvailabilityChangedEventHandler : INotificationHandler<FlightRateAvailabilityChangedEvent>
    {
        public Task Handle(FlightRateAvailabilityChangedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($" Flight - {notification.Flight.Id}, from {notification.Flight.OriginAirportId} to {notification.Flight.DestinationAirportId} -  Availability Changed by {notification.Flight.OriginAirportId} !");
            return Task.FromResult(true);
        }
    }
}
