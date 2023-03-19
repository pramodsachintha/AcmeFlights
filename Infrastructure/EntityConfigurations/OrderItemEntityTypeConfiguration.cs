using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class OrderItemEntityTypeConfiguration : BaseEntityTypeConfiguration<OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder
            .Property("OrderId")
            .IsRequired();

            builder
            .Property("_destinationAirport")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("DestinationAirport")
            .IsRequired();

            builder
           .Property("_originAirport")
           .UsePropertyAccessMode(PropertyAccessMode.Field)
           .HasColumnName("OriginAirport")
           .IsRequired();

            builder
            .Property("_unitPrice")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("UnitPrice")
            .IsRequired();

            builder
            .Property("_units")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Units")
            .IsRequired();

            builder.HasOne<Flight>()
            .WithMany()
            .IsRequired()
            .HasForeignKey("FlightId");

            builder.HasOne<FlightRate>()
            .WithMany()
            .IsRequired()
            .HasForeignKey("RateId");
        }
    }
}
