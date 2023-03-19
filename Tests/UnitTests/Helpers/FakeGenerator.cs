using API.Application.Commands;
using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Domain.Common;

namespace UnitTests.Helpers
{
    public static class FakeGenerator
    {

        public static CreateOrderDraftCommand MockDraftOrder()
        {
            return new CreateOrderDraftCommand(1313, false, new decimal(0.15), MockBookigItems());
        }

        public static IList<BookingItem> MockBookigItems()
        {
            var bookingItems = new List<BookingItem>()
            {
                new BookingItem() { FlightId = FakeFlightId(), RateId = FakeRateId(), DestinationAirport = "CMU", OriginAirport = "JFK", UnitPrice = 12343, Units = 2 }
            };
            return bookingItems;
        }

        public static Order FakeOrder()
        {
            return new Order(true, new decimal(0.15));
        }

        public static Guid FakeFlightId()
        {
            return new Guid("86b53e87-de7e-471e-b965-a9ce618b3c85");
        }

        public static Guid FakeRateId()
        {
            return new Guid("914b09d9-8f8a-4119-ae32-d0b68c61049e");
        }

        public static Order FakeOrderWihItems(Guid flightId, Guid rateId)
        {
            var order = new Order(true, new decimal(0.15));
            order.AddOrderItem(flightId, rateId, "CMU", "JFK", new decimal(21312), 2);
            return order;
        }

        public static Tuple<Flight, Order> FakeFlightAndOrder(int availability)
        {
            var flight = new Flight(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, Guid.NewGuid(), Guid.NewGuid());
            flight.AddRate("First Class", new Price(new decimal(12313), Currency.EUR), availability);
            var order = FakeOrderWihItems(flight.Id, flight.Rates.FirstOrDefault().Id);
            return Tuple.Create(flight, order);
        }

        public static IEnumerable<Flight> FakeFlights()
        {
            var firstFlight = new Flight(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), Guid.NewGuid(), Guid.NewGuid());
            firstFlight.AddRate("First Class", new Price(new decimal(12313), Currency.EUR), 2);
            firstFlight.AddRate("Economy Class", new Price(new decimal(3222), Currency.EUR), 55);

            var secondFlight = new Flight(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(2), Guid.NewGuid(), Guid.NewGuid());
            secondFlight.AddRate("First Class", new Price(new decimal(22222), Currency.EUR), 2);
            secondFlight.AddRate("Economy Class", new Price(new decimal(2113), Currency.EUR), 55);

            var flights = new List<Flight>
            {
                firstFlight, secondFlight
            };
            return flights;
        }

        public static IEnumerable<Airport> FakeAirpots()
        {
            var airports = new List<Airport>()
            {
                new Airport("AMS", "Amsterdam Airport Schiphol"),
                new Airport("FRA", "Frankfurt am Main Airport"),
                new Airport("LHR", "London Heathrow Airport"),
                new Airport("BCN", "Barcelona International Airport"),
                new Airport("CDG", "Charles de Gaulle International"),
                new Airport("IST", "Istanbul Airport"),
                new Airport("MUC", "Munich Airport"),
                new Airport("ZRH", "Zurich Airport"),
                new Airport("DUB", "Dublin Airport"),
                new Airport("FCO", "Rome Fiumicino Airport"),
                new Airport("ARN", "Stockholm Arlanda Airport"),
                new Airport("CPH", "Copenhagen Airport"),
            };
            return airports;
        }
    }
}
