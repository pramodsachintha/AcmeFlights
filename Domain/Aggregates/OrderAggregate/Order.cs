using Domain.Events;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        private DateTime _orderDate;

        private decimal _totalPrice;

        private int _orderStatusId;

        private decimal _taxRate;

        public bool IsRoundTrip { get; private set; }
        public OrderStatus OrderStatus
        {
            get;
            private set;
        }

        private readonly List<OrderItem> _orderItems;

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public Order(bool isRoundTrip, decimal taxRate) : this()
        {
            _orderStatusId = OrderStatus.Draft.Id;
            _orderDate = DateTime.UtcNow;
            _taxRate = taxRate;
            IsRoundTrip = isRoundTrip;
        }

        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public void AddOrderItem(Guid flightId, Guid rateId, string originAirport, string destinationAirport, decimal unitPrice, int units = 1)
        {
            var existingOrderForFlight = _orderItems.SingleOrDefault(o => o.FlightId == flightId);

            if (_orderStatusId != OrderStatus.Draft.Id)
            {
                throw new InvalidOperationException("Order has already been confirmed!");
            }

            if (existingOrderForFlight is null)
            {
                var newOrderItem = new OrderItem(flightId, rateId, originAirport, destinationAirport, unitPrice, units);
                _orderItems.Add(newOrderItem);
            }
            else
            {
                existingOrderForFlight.ChangeUnits(units);
            }

            // Re calculate the Total Price
            _totalPrice = GetTotal();

        }

        /// <summary>
        /// flat tax amount added on top of total price
        /// </summary>
        /// <returns>decimal</returns>
        public decimal GetTotal()
        {
            var totalSum = _orderItems.Sum(o => o.GetUnits() * o.GetUnitPrice());
            var taxAmount = totalSum * _taxRate;
            return totalSum + taxAmount;
        }

        public void SetOrderConfirmedStatus()
        {
            if (_orderStatusId == OrderStatus.Draft.Id)
            {
                _orderStatusId = OrderStatus.Confrimed.Id;
                AddDomainEvent(new OrderStatusChangedToConfirmedDomainEvent(Id, _orderItems));
            }
        }

    }
}
