using Domain.Aggregates.AirportAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Commands
{
    public class CreateAirportCommandHandler : IRequestHandler<CreateAirportCommand, Airport>
    {
        private readonly IAirportRepository _airportRepository;

        public CreateAirportCommandHandler(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public async Task<Airport> Handle(CreateAirportCommand request, CancellationToken cancellationToken)
        {
            var airport = _airportRepository.Add(new Airport(request.Code, request.Name));

            await _airportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return airport;
        }
    }
}