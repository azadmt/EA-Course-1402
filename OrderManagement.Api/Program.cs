using Framework.Core.Persistence;
using Framework.Core;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Persistence.EF;
using OrderManagement.Application;
using OrderManagement.Domain.Order;
using OrderManagement.Persistence.EF.Repository;
using MassTransit;
using Framework.Core.Domain;
using Scrutor;

namespace OrderManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services
             .AddDbContext<OrderManagementDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("default")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddScoped<ICommandHandler<CreateOrderCommand>, CreateOrderCommandHandler>();
            //builder.Services.AddScoped<ICommandHandler<AddNewItemsToOrderCommand>, AddNewItemsToOrderCommandHandler>();
            //  builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IGuidProvider, GuidProvider>();

            builder.Services.Scan(scan => scan
              .FromAssemblyOf<OrderRepository>() // 1. Find the concrete classes
              .AddClasses()        //    to register
              .UsingRegistrationStrategy(RegistrationStrategy.Skip) // 2. Define how to handle duplicates
              .AsMatchingInterface()    // 2. Specify which services they are registered as
              .WithScopedLifetime()); // 3. Set the lifetime for the services

            // builder.Services.Scan(selector => selector
            //    .FromAssemblyOf<OrderRepository>()
            //    .AddClasses(classSelector =>
            //        classSelector.AssignableTo<IOrderRepository>())
            //    .AsMatchingInterface()
            //    .WithScopedLifetime()
            //);

            builder.Services.Scan(scan => scan
            .FromAssemblies(typeof(CreateOrderCommandHandler).Assembly)
            .AddClasses(classes =>
                classes.AssignableTo(typeof(ICommandHandler<>))
                    .Where(c => !c.IsAbstract && !c.IsGenericTypeDefinition))
            .AsSelfWithInterfaces()
            .WithLifetime(ServiceLifetime.Scoped)
        );
            builder.Services.AddScoped<ICommandBus, Framework.Core.Bus>();
            builder.Services.AddSingleton<IEventBus, Framework.Bus.MassTransit.MasstransitBus>();

            builder.Services.AddScoped<IUnitOfWork, OrderManagementDbContext>();

            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "ea", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}