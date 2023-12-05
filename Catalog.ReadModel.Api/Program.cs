using Catalog.Domain.Contract;
using Catalog.ReadModel.Api.EventHandler;
using MassTransit;

namespace Catalog.ReadModel.Api
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
            builder.Services.AddMassTransit(x =>
            {
                //// TODO: Auto Register Consumers
                x.AddConsumer<ProductCategoryCreatedEventHandler>();
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