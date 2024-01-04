using MassTransit;
using Newtonsoft.Json.Linq;

public class CorrelationIdPublishFilter<T> :
     IFilter<PublishContext<T>>
     where T : class
{
    public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
    {
        context.CorrelationId = Guid.NewGuid();
        return next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}