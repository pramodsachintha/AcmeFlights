using API.Application.Commands;
using API.Controllers;
using API.Mapping;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Controllers
{
    public class OrderControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<OrderController>> _loggerMock;
        private Mapper _mapper;

        public OrderControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<OrderController>>();
            var orderProfile = new OrderProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(orderProfile));
            _mapper = new Mapper(configuration);
        }

        [Fact]
        public async Task Confirml_order_with_orderId_success()
        {
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<ConfirmOrderCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(FakeGenerator.FakeOrder()));

            //Act
            var orderController = new OrderController(_loggerMock.Object, _mediatorMock.Object, _mapper);
            var result = await orderController.ConfirmOrderAsync(new ConfirmOrderCommand(Guid.NewGuid()));
            var actionResult = result.Result as OkObjectResult;

            //Assert
            Assert.Equal((int)System.Net.HttpStatusCode.OK, actionResult.StatusCode);

        }
    }
}
