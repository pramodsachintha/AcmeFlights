using Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : BaseEntityTypeConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);


            builder
            .Property("IsRoundTrip")
            .IsRequired();

            builder
            .Property("_orderDate")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("OrderDate")
            .IsRequired();

            builder
            .Property("_totalPrice")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("TotalPrice")
            .IsRequired();

            builder
            .Property<int>("_orderStatusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("OrderStatusId")
            .IsRequired();

            builder
            .Property("_taxRate")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("TaxRate")
            .IsRequired();

            builder
            .HasOne(o => o.OrderStatus)
            .WithMany()
            .HasForeignKey("_orderStatusId");

        }

    }
}
