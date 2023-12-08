using Catalog.Application;
using Catalog.Application.DataContract;
using Catalog.Application.Product;
using Catalog.Application.ProductCategory;
using Catalog.Domaim.Product;
using Catalog.Domaim.ProductCategory;
using Catalog.Persistence.EF;
using Catalog.Persistence.EF.Repository;
using Framework.Bus.MassTransit;
using Framework.Core;
using Framework.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using MassTransit;

namespace Catalog.WebApi
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
            builder.Services.AddScoped<ICommandHandler<CreateProductCommand>, CreateProductCommandHandler>();
            builder.Services.AddScoped<ICommandHandler<CreateProductCategoryCommand>, ProductCategoryCommandHandler>();
            builder.Services.AddScoped<ICommandBus, Framework.Core.Bus>();
            builder.Services.AddScoped<IEventBus, MassTransitBusImp>();
            builder.Services.AddScoped<IProductRepository, ProductAggregateRepository>();
            builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            builder.Services.AddScoped<IUnitOfWork, ProductCatalogDbContext>();

            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                });
            });

            builder.Services.AddMassTransitHostedService();
            builder.Services
                .AddDbContext<ProductCatalogDbContext>(x =>
                x
                .UseSqlServer(builder.Configuration.GetConnectionString("default")));

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