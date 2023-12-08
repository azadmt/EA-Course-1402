using Framework.Core;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace Framework.OutboxPublisher
{
    public partial class OutboxManager
    {
        private readonly IDbConnection _dbConnection;
        private readonly IEventBus _eventBus;

        public OutboxManager(IDbConnection dbConnectionm, IEventBus eventBus)
        {
            _dbConnection = dbConnectionm;
            _eventBus = eventBus;
        }

        public void Start(Assembly[] eventAssemblies)
        {
            var notSyncedOutbox = _dbConnection.GetOutboxes();
            if (notSyncedOutbox != null)
            {
                foreach (var item in notSyncedOutbox)
                {
                    _eventBus.Publish(ToEvent(item));
                    Console.WriteLine($"Publish Outbox :{item.EventBody}");
                }
                _dbConnection.UpdatePublishedDate(notSyncedOutbox.Select(x => x.Id));
            }
        }

        private object ToEvent(OutboxItem outboxItem)
        {
            Type type = Type.GetType(outboxItem.EventType);
            var @event = JsonConvert.DeserializeObject(outboxItem.EventBody, type, new JsonSerializerSettings
            {
                ContractResolver = new NonPublicPropertiesResolver()
            });
            return @event;
        }
    }
}