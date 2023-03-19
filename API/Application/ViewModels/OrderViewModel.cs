using System;

namespace API.Application.ViewModels
{
    public record OrderViewModel
    {
        public Guid OrderId { get; init; }
        public decimal Total { get; init; }

        public bool IsRoundTrip { get; init; }

    }
}
