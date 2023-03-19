using API.Application.Commands;
using API.Application.Dto;
using API.Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public OrderController(
        ILogger<OrderController> logger,
        IMediator mediator,
        IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderDraftDto>> CreateOrderAsync([FromBody] CreateOrderDraftCommand createOrderDraftCommand)
        {
            _logger.LogInformation("Command Initiated. -  {CommandName} - {Id}", nameof(CreateOrderDraftCommand), createOrderDraftCommand.CustomerId);
            var order = await _mediator.Send(createOrderDraftCommand);
            return Ok(order);
        }

        [Route("")]
        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderDraftDto>> UpdateOrderItemsAsync([FromBody] UpdateOrderItemsCommand createOrderDraftCommand)
        {
            _logger.LogInformation("Command Initiated. -  {CommandName} - {Id}", nameof(UpdateOrderItemsCommand), createOrderDraftCommand.OrderId);
            var order = await _mediator.Send(createOrderDraftCommand);
            return Ok(_mapper.Map<OrderViewModel>(order));
        }

        [Route("confirm")]
        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderViewModel>> ConfirmOrderAsync([FromBody] ConfirmOrderCommand confirmOrderCommand)
        {
            _logger.LogInformation("Command Initiated. -  {CommandName} - {Id}", nameof(ConfirmOrderCommand), confirmOrderCommand.OrderId);
            var order = await _mediator.Send(confirmOrderCommand);
            return Ok(_mapper.Map<OrderViewModel>(order));
        }
    }
}
