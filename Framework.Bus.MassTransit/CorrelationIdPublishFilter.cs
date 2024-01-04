using MassTransit;
using Newtonsoft.Json.Linq;

public class CorrelationIdPublishFilter<T> :
     IFilter<PublishContext<T>>
     where T : class
{
    private Serilog.ILogger _logger;

    public CorrelationIdPublishFilter(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
    {
        context.CorrelationId = Guid.NewGuid();
        next.Send(context);

        return Task.CompletedTask;
    }

    public void Probe(ProbeContext context)
    {
    }
}

public class PublishLogFilter<T> :
     IFilter<PublishContext<T>>
     where T : class
{
    private Serilog.ILogger _logger;

    public PublishLogFilter(Serilog.ILogger logger)
    {
        _logger = logger;
    }

    public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
    {
        next.Send(context);
        _logger.Information("publish {@EventType} {@CorrelationId}   {@Message} ",
        context.Message.GetType(),
        context.CorrelationId,
        context.Message);
        return Task.CompletedTask;
    }

    public void Probe(ProbeContext context)
    {
    }
}