using Catalog.Domain.Contract;
using Catalog.ReadModel.Api.EventHandler;
using MassTransit;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using Serilog;
using Framework.Bus.MassTransit;

namespace Catalog.ReadModel.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Host.UseSerilog((ctx, lc) =>
                        lc
                  .WriteTo.Console()
                  .WriteTo.Seq("http://localhost:5341"))
              ;

            var logger = new LoggerConfiguration()
                         .WriteTo.Console()
                         .WriteTo.Seq("http://localhost:5341")
                            .CreateLogger();
            //builder.Logging.ClearProviders();
            //builder.Logging.AddSerilog(logger);
            // builder.Services.AddSingleton<Serilog.ILogger>(()=> logger);
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
                    cfg.ConfigureJsonSerializerOptions(options =>
                    {
                        // customize the JsonSerializerOptions here
                        return options;
                    });
                    cfg.ConfigureEndpoints(context);
                    cfg.UseConsumeFilter(typeof(MyConsumeLogFilter<>), context);

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
            app.UseSerilogRequestLogging();
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

    public class NonPublicPropertiesResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);
            if (member is PropertyInfo pi)
            {
                prop.Readable = (pi.GetMethod != null);
                prop.Writable = (pi.SetMethod != null);
            }
            return prop;
        }
    }
}