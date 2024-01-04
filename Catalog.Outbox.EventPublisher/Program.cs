using Framework.Bus.MassTransit;
using Framework.Core;
using Framework.OutboxEventPublisher;
using MassTransit;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace Catalog.Outbox.EventPublisher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                  .UseSerilog((ctx, lc) =>
                        lc
                  .Enrich.WithMachineName()
                  .WriteTo.Console()
                  .WriteTo.Seq("http://localhost:5341"))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IDbConnection>(new SqlConnection(hostContext.Configuration.GetConnectionString("default")));
                    services.AddSingleton<IEventBus, MasstransitBus>();
                    services.AddSingleton<OutboxManager>();

                    services.AddMassTransit(x =>
                    {
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.UsePublishFilter(typeof(CorrelationIdPublishFilter<>), context);
                            cfg.UsePublishFilter(typeof(PublishLogFilter<>), context);
                            cfg.Host("localhost", "ea", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });
                        });
                    });
                    services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
    }
}