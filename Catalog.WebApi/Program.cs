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
            builder.Services.AddScoped<ICommandBus, Bus>();
            builder.Services.AddScoped<IProductRepository, ProductAggregateRepository>();
            builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            builder.Services.AddScoped<IUnitOfWork, ProductCatalogDbContext>();
            builder.Services
                 .AddDbContext<ProductCatalogDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("default")))
                // .AddDbContext<ProductCatalogDbContext>(opt => opt.UseInMemoryDatabase(nameof(ProductCatalogDbContext)))

                ;

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