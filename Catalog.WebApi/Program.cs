using Catalog.Application;
using Catalog.Application.DataContract;
using Catalog.Application.DataContract.Product;
using Catalog.Application.DataContract.ProductCategory;
using Catalog.Application.Product;
using Catalog.Application.ProductCategory;
using Catalog.Domain.Product;
using Catalog.Domain.ProductCategory;
using Catalog.Persistence.EF;
using Catalog.Persistence.EF.Repository;
using Framework.Core;
using Framework.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Framework.Bus.MassTransit;
using Catalog.Domain.Contract;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.Extensions.DependencyInjection;

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
            //TODO Implement Auto register command handlers
            builder.Services.AddScoped<ICommandHandler<CreateProductCommand>, CreateProductCommandHandler>();
            builder.Services.AddScoped<ICommandHandler<CreateProductCategoryCommand>, ProductCategoryCommandHandler>();
            builder.Services.AddScoped<ICommandBus, Framework.Core.Bus>();
            builder.Services.AddScoped<IEventBus, MasstransitBus>();
            builder.Services.AddScoped<IProductRepository, ProductAggregateRepository>();
            builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            builder.Services.AddScoped<IUnitOfWork, ProductCatalogDbContext>();
            builder.Services
                 .AddDbContext<ProductCatalogDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("default")))
                // .AddDbContext<ProductCatalogDbContext>(opt => opt.UseInMemoryDatabase(nameof(ProductCatalogDbContext)))

                ;

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

            //  builder.Services.AddMassTransitHostedService();
            AddHealthCheck(builder.Services, builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //Sets the health endpoint
                endpoints.MapHealthChecksUI();
            });
            app.UseHealthChecksUI();
            app.MapControllers();
            app.MapHealthChecks("/hc");
            app.MapHealthChecks("/hc-d", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI();
            app.Run();
        }

        private static void AddHealthCheck(IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();
            hcBuilder.AddSqlServer(configuration.GetConnectionString("default"));
            hcBuilder.AddRabbitMQ(new Uri("amqp://guest:guest@localhost:5672"));
        }
    }
}