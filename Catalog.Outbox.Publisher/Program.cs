using Catalog.Outbox.Publisher;
using Framework.Core;
using Framework.OutboxPublisher;
using MassTransit;
using System.Data;
using System.Data.SqlClient;

IHost host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton<IDbConnection>(new SqlConnection(hostContext.Configuration.GetConnectionString("default")));
                        services.AddSingleton<IEventBus, Framework.Bus.MassTransit.MassTransitBusImp>();
                        services.AddSingleton<OutboxManager>();
                        services.AddMassTransit(x =>
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
                        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
