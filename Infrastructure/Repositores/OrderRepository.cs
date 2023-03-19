using Domain.Aggregates.OrderAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FlightsContext _context;
        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public OrderRepository(FlightsContext context)
        {
            _context = context;
        }

        public Order Add(Order order)
        {
            return _context.Orders.Add(order).Entity;
        }

        public async Task<Order> GetAsync(Guid orderId)
        {
            var order = await _context
                           .Orders
                           .SingleOrDefaultAsync(o => o.Id == orderId);

            await _context.Entry(order)
                .Collection(i => i.OrderItems).LoadAsync();

            await _context.Entry(order)
                .Reference(i => i.OrderStatus).LoadAsync();

            return order;
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}
