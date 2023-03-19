using System;

namespace API.Application.ViewModels
{
    public record OrderItemViewModel
    {
        public Guid FlightId { get; init; }

        public string Description { get; init; }

        public decimal UnitPrice { get; init; }

        public string OriginAirport { get; init; }

        public string DestinationAirport { get; init; }

        public int Units { get; init; }
    }
}
