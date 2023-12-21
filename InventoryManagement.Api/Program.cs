using InventoryManagement.Api.Handler;
using InventoryManagement.Api.Service;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace InventoryManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<InventoryDbContext>(opt => opt.UseInMemoryDatabase("InventoryManagementDb"));
            builder.Services.AddMassTransit(x =>
            {
                //// TODO: Auto Register Consumers
                x.AddConsumer<OrderCreatedEventHandler>();
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
            builder.Services.AddMassTransitHostedService();
            builder.Services.AddScoped<StockService>();
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