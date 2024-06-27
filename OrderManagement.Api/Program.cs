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
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using OpenTelemetry.Exporter;

namespace OrderManagement.Api
{
    public class Program
    {
        private static string serviceName = "OrderManagement";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services
             .AddDbContext<OrderManagementDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("default")));

            //builder.Logging.AddOpenTelemetry(options =>
            //{
            //    options
            //        .SetResourceBuilder(
            //            ResourceBuilder.CreateDefault()
            //                .AddService(serviceName))
            //        .AddConsoleExporter();
            //});
            //builder.Services.AddOpenTelemetry()
            //      .ConfigureResource(resource => resource.AddService(serviceName))
            //      .WithTracing(tracing => tracing
            //          .AddAspNetCoreInstrumentation()
            //          .AddConsoleExporter())
            //      .WithMetrics(metrics => metrics
            //          .AddAspNetCoreInstrumentation()
            //          .AddConsoleExporter());

            builder.Services.AddLogging(logging => logging.AddOpenTelemetry(openTelemetryLoggerOptions =>
            {
                openTelemetryLoggerOptions.SetResourceBuilder(
                    ResourceBuilder.CreateEmpty()
                        // Replace "GettingStarted" with the name of your application
                        .AddService("GettingStarted")
                        .AddAttributes(new Dictionary<string, object>
                        {
                            // Add any desired resource attributes here
                            ["deployment.environment"] = "development"
                        }));

                // Some important options to improve data quality
                openTelemetryLoggerOptions.IncludeScopes = true;
                openTelemetryLoggerOptions.IncludeFormattedMessage = true;

                openTelemetryLoggerOptions.AddOtlpExporter(exporter =>
                {
                    // The full endpoint path is required here, when using
                    // the `HttpProtobuf` protocol option.
                    exporter.Endpoint = new Uri("http://localhost:5341/ingest/otlp/v1/logs");
                    exporter.Protocol = OtlpExportProtocol.HttpProtobuf;
                    // Optional `X-Seq-ApiKey` header for authentication, if required
                    exporter.Headers = "X-Seq-ApiKey=heWJdT5klSVqdPwR7ejI";
                });
            }));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IGuidProvider, GuidProvider>();

            builder.Services.Scan(scan => scan
              .FromAssemblyOf<OrderRepository>() // 1. Find the concrete classes
              .AddClasses()        //    to register
              .UsingRegistrationStrategy(RegistrationStrategy.Skip) // 2. Define how to handle duplicates
              .AsMatchingInterface()    // 2. Specify which services they are registered as
              .WithScopedLifetime()); // 3. Set the lifetime for the services

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