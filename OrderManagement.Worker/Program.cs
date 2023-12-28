using Framework.Core;
using Framework.Core.Domain;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Application;
using OrderManagement.Persistence.EF;
using OrderManagement.Persistence.EF.Repository;
using OrderManagement.Worker.EventHandler;
using Scrutor;

namespace OrderManagement.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices((hostContext, services) =>
                 {
                     // Add services to the container.
                     services
                       .AddDbContext<OrderManagementDbContext>(opt => opt.UseSqlServer(hostContext.Configuration.GetConnectionString("default")));

                     services.AddScoped<IGuidProvider, GuidProvider>();

                     services.Scan(scan => scan
                       .FromAssemblyOf<OrderRepository>() // 1. Find the concrete classes
                       .AddClasses()        //    to register
                       .UsingRegistrationStrategy(RegistrationStrategy.Skip) // 2. Define how to handle duplicates
                       .AsMatchingInterface()    // 2. Specify which services they are registered as
                       .WithScopedLifetime()); // 3. Set the lifetime for the services

                     services.Scan(scan => scan
                     .FromAssemblies(typeof(CreateOrderCommandHandler).Assembly)
                     .AddClasses(classes =>
                         classes.AssignableTo(typeof(ICommandHandler<>))
                             .Where(c => !c.IsAbstract && !c.IsGenericTypeDefinition))
                     .AsSelfWithInterfaces()
                     .WithLifetime(ServiceLifetime.Scoped)
                 );
                     services.AddScoped<ICommandBus, Framework.Core.Bus>();

                     services.AddMassTransit(x =>
                     {
                         //// TODO: Auto Register Consumers
                         x.AddConsumer<StockAdjusmentRejectedEventHandler>();
                         x.AddConsumer<StockAdjusmentConfirmedEventHandler>();
                         x.UsingRabbitMq((context, cfg) =>
                         {
                             cfg.ConfigureEndpoints(context);

                             //cfg.ReceiveEndpoint(nameof(ProductCategoryCreatedEvent), e =>
                             //{
                             //    e.ConfigureConsumer<ProductCategoryCreatedEventHandler>(context);
                             //});
                             cfg.Host("localhost", "ea", h =>
                             {
                                 h.Username("guest");
                                 h.Password("guest");
                             });
                         });
                     });
                     services.AddHostedService<Worker>();
                 })
                .Build();

            host.Run();
        }
    }
}