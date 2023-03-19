using API.Application.Queries;
using AutoMapper;
using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.FlightAggregate;
using Moq;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Handlers
{
    public class QueryHandlerTests
    {
        private readonly Mock<IAirportRepository> _airportRepository;
        private readonly Mock<IFlightRepository> _flightRepostoryMock;
        private Mapper _mapper;

        public QueryHandlerTests()
        {
            _flightRepostoryMock = new Mock<IFlightRepository>();
            _airportRepository = new Mock<IAirportRepository>();
        }

        [Fact]
        public async Task Handle_return_not_null_flight_details_for_correct_destination()
        {
            //Arrange
            var flights = FakeGenerator.FakeFlights();
            var destination = new Airport("AMS", "Amsterdam Airport Schiphol");

            _flightRepostoryMock.Setup(o => o.FindByDestinationAirportId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(flights));

            _airportRepository.Setup(o => o.FindAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(destination));

            _airportRepository.Setup(o => o.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(FakeGenerator.FakeAirpots().Last()));

            //Act
            var handler = new FlightsByDestinationQueryHandler(_flightRepostoryMock.Object, _airportRepository.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(new FlightsByDestinationQuery("AMS"), cltToken);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public async Task Handle_return_lowest_flight_rates_for_destination()
        {
            //Arrange
            var flights = FakeGenerator.FakeFlights();
            var destination = new Airport("AMS", "Amsterdam Airport Schiphol");

            _flightRepostoryMock.Setup(o => o.FindByDestinationAirportId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(flights));

            _airportRepository.Setup(o => o.FindAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(destination));

            _airportRepository.Setup(o => o.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(FakeGenerator.FakeAirpots().Last()));

            //Act
            var handler = new FlightsByDestinationQueryHandler(_flightRepostoryMock.Object, _airportRepository.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(new FlightsByDestinationQuery("AMS"), cltToken);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("3222", result.First().PriceFrom.ToString());
            Assert.Equal("2113", result.Last().PriceFrom.ToString());
        }
    }
}
