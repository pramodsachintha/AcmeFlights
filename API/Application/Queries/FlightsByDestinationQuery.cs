using API.ApiResponses;
using MediatR;
using System.Collections.Generic;

namespace API.Application.Queries
{
    public class FlightsByDestinationQuery : IRequest<IEnumerable<FlightResponse>>
    {
        public string Destination { get; private set; }

        public FlightsByDestinationQuery(string destination)
        {
            Destination = destination;
        }

    }
}
