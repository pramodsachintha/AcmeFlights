using Domain.Exceptions;
using Domain.SeedWork;
using System;

namespace Domain.Aggregates.OrderAggregate
{
    /// <summary>
    /// This class reflects the list of flights that is being booked
    /// </summary>
    public class OrderItem : Entity
    {
        /// <summary>
        /// Associated Flight ID
        /// </summary>
        public Guid FlightId { get; private set; }

        /// <summary>
        /// Rate Id Associated with the flight
        /// </summary>
        public Guid RateId { get; private set; }


        private string _originAirport;

        private string _destinationAirport;

        /// <summary>
        /// Number of Seats that is being booked
        /// </summary>
        private int _units;

        /// <summary>
        /// Price per Seat
        /// </summary>
        private decimal _unitPrice;

        public OrderItem(Guid flightId, Guid rateId, string originAirport, string destinationAirport, decimal unitPrice, int units = 1)
        {
            if (units <= 0)
            {
                throw new OrderingDomainException("Invalid Ticket Count");
            }

            FlightId = flightId;
            RateId = rateId;
            _originAirport = originAirport;
            _destinationAirport = destinationAirport;
            _unitPrice = unitPrice;
            _units = units;
        }

        public int GetUnits() => _units;

        public string GetOriginAirport() => _originAirport;

        public string GetDestinationAirport() => _destinationAirport;

        public decimal GetUnitPrice() => _unitPrice;

        public string GetItemDescription() => $"{_originAirport} to {_destinationAirport}";

        public void ChangeUnits(int units)
        {
            if (units < 0)
            {
                throw new OrderingDomainException("Invalid units");
            }

            _units = units;
        }

    }
}
