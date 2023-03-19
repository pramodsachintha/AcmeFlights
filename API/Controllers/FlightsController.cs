using API.ApiResponses;
using API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController : ControllerBase
{
    private readonly ILogger<FlightsController> _logger;
    private readonly IMediator _mediator;

    public FlightsController(
        ILogger<FlightsController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("Search/{destination}")]
    [ProducesResponseType(typeof(FlightResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAvailableFlights(string destination)
    {
        _logger.LogInformation("Query Initiated. -  {QueryName} - {Id}", nameof(FlightsByDestinationQuery), destination);
        var flights = await _mediator.Send(new FlightsByDestinationQuery(destination));
        return Ok(flights);
    }
}
