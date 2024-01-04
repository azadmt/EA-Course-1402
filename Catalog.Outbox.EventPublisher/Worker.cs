using Catalog.Domain.Contract;
using Framework.OutboxEventPublisher;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Reflection;
using System.Timers;

namespace Catalog.Outbox.EventPublisher
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly OutboxManager _outboxManager;

        public Worker(ILogger<Worker> logger, OutboxManager outboxManager)
        {
            _logger = logger;
            _outboxManager = outboxManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(CheckOutBox);
            aTimer.Interval = 3000;
            aTimer.Enabled = true;

            //CheckOutBox(default(object), default(ElapsedEventArgs));
        }

        private void CheckOutBox(object? sender, ElapsedEventArgs e)
        {
            var contractAssembies = new Assembly[] { typeof(ProductCategoryCreatedEvent).Assembly };
            _outboxManager.Start(contractAssembies);
        }
    }
}