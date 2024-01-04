using MassTransit;
using System.Diagnostics;

namespace Framework.Bus.MassTransit
{
    public class MyConsumeLogFilter<T> :
IFilter<ConsumeContext<T>>
where T : class
    {
        private Serilog.ILogger _logger;
        private Stopwatch stopwatch;

        public MyConsumeLogFilter(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            _logger.Information("Start Processe {@EventType} {@CorrelationId}   {@Message} ",
                context.Message.GetType(),
                context.CorrelationId,
                context.Message);
            stopwatch = Stopwatch.StartNew();
            await next.Send(context);
            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            _logger.Information("Finish Processe {@EventType} {@CorrelationId} : {@elapsedMilliseconds} ms ",
             context.Message.GetType(),
             context.CorrelationId,
             elapsedMilliseconds);
        }

        public void Probe(ProbeContext context)
        { }
    }
}