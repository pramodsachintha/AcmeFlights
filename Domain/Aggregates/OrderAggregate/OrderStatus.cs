using Domain.Exceptions;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Aggregates.OrderAggregate
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Draft = new OrderStatus(1, nameof(Draft).ToLowerInvariant());
        public static OrderStatus Confrimed = new OrderStatus(2, nameof(Confrimed).ToLowerInvariant());
        public static OrderStatus Paid = new OrderStatus(3, nameof(Paid).ToLowerInvariant());
        public static OrderStatus Cancelled = new OrderStatus(4, nameof(Cancelled).ToLowerInvariant());

        public OrderStatus(int id, string name) : base(id, name)
        {
        }

        public static IEnumerable<OrderStatus> List() =>
            new[] { Draft, Confrimed, Paid, Cancelled };

        public static OrderStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new OrderingDomainException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static OrderStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new OrderingDomainException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
