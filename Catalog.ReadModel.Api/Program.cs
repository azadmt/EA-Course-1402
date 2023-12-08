using Catalog.ReadModel.Api;
using Catalog.ReadModel.Api.EventHandlers;
using MassTransit;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDbConnection>( new SqlConnection(builder.Configuration.GetConnectionString("default")));
builder.Services.AddScoped<ProductCategoryDbManager>();
builder.Services.AddScoped<InboxDbManager>();
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
        cfg.Host("localhost", "/", h =>
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
