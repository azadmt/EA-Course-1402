using Catalog.Domain.Contract;
using Framework.OutboxPublisher;
using System.Reflection;
using System.Timers;

namespace Catalog.Outbox.Publisher
{
    public class Worker : BackgroundService
    {
       
        private readonly OutboxManager outboxManager;

        public Worker(OutboxManager outboxManager)
        {
       
            this.outboxManager = outboxManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(CheckOutBox);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;
            //CheckOutBox(null, null);
        }

        private void CheckOutBox(object? sender, ElapsedEventArgs e)
        {
            var contractAssembies = new Assembly[] { typeof(ProductCategoryCreatedEvent).Assembly };
            outboxManager.Start(contractAssembies);
        }
    }
}