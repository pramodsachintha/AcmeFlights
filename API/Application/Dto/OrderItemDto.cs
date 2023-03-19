using System;

namespace API.Application.Dto
{
    public record OrderItemDto
    {
        public Guid FlightId { get; init; }

        public Guid RateId { get; init; }

        public decimal UnitPrice { get; init; }

        public string OriginAirport { get; init; }

        public string DestinationAirport { get; init; }

        public int Units { get; init; }
    }
}
