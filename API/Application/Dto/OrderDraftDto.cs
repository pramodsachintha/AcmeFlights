using System;
using System.Collections.Generic;

namespace API.Application.Dto
{
    public record OrderDraftDto
    {
        public Guid OrderId { get; init; }

        public string OrderStatus { get; init; }

        public decimal Total { get; init; }

        public bool IsRoundTrip { get; init; }

        public IEnumerable<OrderItemDto> OrderItems { get; init; }
    }
}
