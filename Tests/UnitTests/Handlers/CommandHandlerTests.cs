using API.Application.Commands;
using API.Mapping;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Domain.Exceptions;
using Moq;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests.Handlers
{
    public class CommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IFlightRepository> _flightRepostoryMock;
        private Mapper _mapper;


        public CommandHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _flightRepostoryMock = new Mock<IFlightRepository>();
            var orderProfile = new OrderProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(orderProfile));
            _mapper = new Mapper(configuration);

        }

        [Fact]
        public async Task Handle_return_not_null_order_if_order_is_created_as_draft()
        {
            _orderRepositoryMock.Setup(o => o.Add(It.IsAny<Order>()))
                .Returns((FakeGenerator.FakeOrder()));

            _orderRepositoryMock.Setup(orderRepo => orderRepo.UnitOfWork.SaveChangesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(1));

            //Act
            var handler = new CreateOrderDraftCommandHandler(_mapper, _orderRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(FakeGenerator.MockDraftOrder(), cltToken);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Handle_throws_exception_when_no_items()
        {
            _orderRepositoryMock.Setup(o => o.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(FakeGenerator.FakeOrder()));

            //Act
            var handler = new ConfirmOrderCommandHandler(_flightRepostoryMock.Object, _orderRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();

            //Assert
            await Assert.ThrowsAsync<OrderingDomainException>(async () => await handler.Handle(new ConfirmOrderCommand(Guid.NewGuid()), cltToken));
        }

        [Fact]
        public async Task Handle_throws_exception_when_no_availability()
        {
            (var flight, var order) = FakeGenerator.FakeFlightAndOrder(1);

            _flightRepostoryMock.Setup(o => o.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(flight));

            _orderRepositoryMock.Setup(o => o.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(order));

            //Act
            var handler = new ConfirmOrderCommandHandler(_flightRepostoryMock.Object, _orderRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();

            //Assert
            await Assert.ThrowsAsync<OrderingDomainException>(async () => await handler.Handle(new ConfirmOrderCommand(Guid.NewGuid()), cltToken));
        }

        [Fact]
        public async Task Handle_returns_order_when_seats_available()
        {
            (var flight, var order) = FakeGenerator.FakeFlightAndOrder(4);

            _flightRepostoryMock.Setup(o => o.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(flight));

            _orderRepositoryMock.Setup(o => o.GetAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(order));

            _orderRepositoryMock.Setup(orderRepo => orderRepo.UnitOfWork.SaveChangesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(1));

            _flightRepostoryMock.Setup(flightRepo => flightRepo.UnitOfWork.SaveChangesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(1));

            //Act
            var handler = new ConfirmOrderCommandHandler(_flightRepostoryMock.Object, _orderRepositoryMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(new ConfirmOrderCommand(default), cltToken);

            //Assert
            Assert.NotNull(result);
        }
    }
}
