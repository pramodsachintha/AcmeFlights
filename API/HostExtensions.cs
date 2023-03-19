using API.Infrastructure.Seeds;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace API
{
    public static class HostExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var configuration = services.GetService<IConfiguration>();

                if (configuration.GetValue<bool>("Database:Seed"))
                {
                    var context = services.GetService<FlightsContext>();

                    new AirportsSeeder(context).Seed();
                    new FlightsSeeder(context).Seed();
                    new FlightRatesSeeder(context).Seed();
                    new OrderStatusSeeder(context).Seed();
                    Console.WriteLine("Database seeded");
                }
            }

            return host;
        }

        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<FlightsContext>();
                context.Database.Migrate();
            }

            return host;
        }

        public static IHost ConnectToDatabase(this IHost host, int maxRetryCount, TimeSpan retryDelay)
        {
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetService<FlightsContext>();
                    var connected = false;
                    var retries = 0;
                    while (!connected)
                    {
                        try
                        {
                            context.Database.CanConnect();
                            connected = true;
                        }
                        catch (Exception)
                        {
                            retries++;
                            if (retries > maxRetryCount)
                            {
                                throw;
                            }

                            Task.Delay(retryDelay).Wait();
                        }
                    }
                }

                return host;
            }
        }
    }
}
