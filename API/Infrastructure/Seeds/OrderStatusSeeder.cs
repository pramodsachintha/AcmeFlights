using Domain.Aggregates.OrderAggregate;
using Infrastructure;
using System;
using System.Linq;

namespace API.Infrastructure.Seeds
{
    public class OrderStatusSeeder : FlightsContextSeeder
    {
        public OrderStatusSeeder(FlightsContext flightsContext) : base(flightsContext)
        {
        }

        public override void Seed()
        {
            if (FlightsContext.OrderStatuses.Any() && FlightsContext.OrderStatuses.Count() == 4)
            {
                Console.WriteLine("Skipping Order Status Seeding");
                return;
            }

            FlightsContext.OrderStatuses.AddRange(OrderStatus.List());
            FlightsContext.SaveChanges();
        }
    }
}
