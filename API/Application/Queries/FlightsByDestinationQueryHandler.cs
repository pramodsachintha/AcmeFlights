using API.ApiResponses;
using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.FlightAggregate;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Queries
{
    public class FlightsByDestinationQueryHandler : IRequestHandler<FlightsByDestinationQuery, IEnumerable<FlightResponse>>
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IAirportRepository _airportRepository;

        public FlightsByDestinationQueryHandler(IFlightRepository flightRepository, IAirportRepository airportRepository)
        {
            _flightRepository = flightRepository;
            _airportRepository = airportRepository;
        }
        public async Task<IEnumerable<FlightResponse>> Handle(FlightsByDestinationQuery request, CancellationToken cancellationToken)
        {
            var flightsResponse = new List<FlightResponse>();
            var destinationAirport = await _airportRepository.FindAsync(request.Destination);

            if (destinationAirport == null)
            {
                return flightsResponse;
            }

            var flights = await _flightRepository.FindByDestinationAirportId(destinationAirport.Id);

            foreach (var flight in flights)
            {
                var originAirport = await _airportRepository.GetAsync(flight.OriginAirportId);
                var lowestPrice = flight.Rates.OrderBy(r => r.Price.Value).FirstOrDefault().Price.Value;
                flightsResponse.Add(new FlightResponse(originAirport.Code, destinationAirport.Code, flight.Departure, flight.Arrival, lowestPrice));
            }

            return flightsResponse;

        }
    }
}
