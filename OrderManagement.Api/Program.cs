using Framework.Core.Persistence;
using Framework.Core;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Persistence.EF;
using OrderManagement.Application;
using OrderManagement.Domain.Order;
using OrderManagement.Persistence.EF.Repository;
using MassTransit;

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
            builder.Services.AddScoped<ICommandHandler<CreateOrderCommand>, CreateOrderCommandHandler>();
            builder.Services.AddScoped<ICommandHandler<AddNewItemsToOrderCommand>, AddNewItemsToOrderCommandHandler>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

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