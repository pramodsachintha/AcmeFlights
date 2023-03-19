using Domain.Aggregates.FlightAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class FlightsRepository : IFlightRepository
    {
        private readonly FlightsContext _context;

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public FlightsRepository(FlightsContext context)
        {
            _context = context;
        }

        public Flight Add(Flight flight)
        {
            return _context.Flights.Add(flight).Entity;
        }

        public void Update(Flight flight)
        {
            _context.Flights.Update(flight);
        }

        public async Task<Flight> GetAsync(Guid flightId)
        {
            return await _context.Flights
                .Include(flight => flight.Rates)
                .SingleOrDefaultAsync(f => f.Id == flightId);
        }

        public async Task<IEnumerable<Flight>> FindByDestinationAirportId(Guid destinationAirportId)
        {
            return await _context.Flights
                .Include(f => f.Rates)
                .AsNoTracking()
                .Where(f => f.Rates.Any() && f.Rates.Any(r => r.Available > 0) && f.DestinationAirportId == destinationAirportId)
                .ToListAsync();
        }
    }
}
