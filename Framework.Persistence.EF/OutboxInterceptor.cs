using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.SqlClient;
using System.Text;
using Newtonsoft.Json;
namespace Framework.Persistence.EF
{
    public class OutboxInterceptor : SaveChangesInterceptor
    {
        public OutboxInterceptor()
        {
        }
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            var outbox = eventData.Context.ChangeTracker
                  .Entries<IAggregateRoot>()
                  .SelectMany(x => x.Entity.GetChanges()).ToList();

            StringBuilder sb = new StringBuilder();
            sb.Append($"INSERT INTO outbox (EventId,EventType,EventBody) VALUES ");
            var paramItems = new List<SqlParameter>();
            for (int i = 0; i < outbox.Count; i++)
            {
                paramItems.Add(new SqlParameter($"@EventId{i}", outbox[i].Id.ToString()));
                paramItems.Add(new SqlParameter($"@EventType{i}", outbox[i].GetType().Name));
                paramItems.Add(new SqlParameter($"@EventBody{i}", JsonConvert.SerializeObject(outbox[i])));

                sb.AppendLine($" (@EventId{i},@EventType{i},@EventBody{i}) ");
                if (i != outbox.Count - 1)
                    sb.Append($" , ");
            }

            eventData.Context
                     .Database
                     .ExecuteSqlRaw("INSERT INTO outbox (EventId,EventType,EventBody) VALUES (@EventId{0},@EventType{0},@EventBody{0})", paramItems);
                    // .ExecuteSqlRaw(sb.ToString(), paramItems);

            return base.SavedChanges(eventData, result);
        }

    }
}
