using API.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace API.Application.Commands
{
    public class CreateOrderDraftCommand : IRequest<OrderDraftDto>
    {
        public int CustomerId { get; private set; }

        public bool IsRoundTrip { get; private set; }

        public decimal TaxRate { get; private set; }

        public IEnumerable<BookingItem> Items { get; private set; }

        public CreateOrderDraftCommand(int customerId, bool isRoundTrip, decimal taxRate, IEnumerable<BookingItem> items)
        {
            CustomerId = customerId;
            IsRoundTrip = isRoundTrip;
            TaxRate = taxRate;
            Items = items;
        }
    }

    public class BookingItem
    {
        public Guid FlightId { get; set; }

        public Guid RateId { get; set; }

        public string OriginAirport { get; set; }

        public string DestinationAirport { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int Units { get; set; }
    }
}
