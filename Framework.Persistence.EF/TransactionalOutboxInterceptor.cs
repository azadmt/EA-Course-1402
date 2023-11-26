using Framework.Core;
using Framework.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Framework.Persistence.EF
{
    internal class TransactionalOutboxInterceptor : SaveChangesInterceptor
    {
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            var dbcontext = (eventData.Context as OutboxSupportApplicationDbContext);
            var outbox = dbcontext
                   .ChangeTracker
                   .Entries<IAggregateRoot>()
                   .Select(entry => entry.Entity)
                       .SelectMany(entity =>
                       {
                           var domainEvents = entity.GetChanges();
                           return domainEvents;
                       })
                       .ToList();

            StringBuilder sb = new StringBuilder();
            sb.Append($" (@Id,@EventType,@EventBody)");
            foreach (var item in outbox)
            {
                object[] paramItems = new object[]
                    {
                        new SqlParameter("@Id", item.Id.ToString()),
                        new SqlParameter("@EventType", item.GetType().Name),
                        new SqlParameter("@EventBody", JsonConvert.SerializeObject(item)),
                    };
                //  sb.Append($"INSERT INTO outbox (Id,EventType,EventBody) VALUES ('{item.Id}','{item.GetType()}','{JsonConvert.SerializeObject(item)}')");

                try
                {
                    //eventData
                    //     .Context
                    //     .Database
                    //     .ExecuteSqlRaw("INSERT INTO outbox (Id,EventType,EventBody) VALUES (@Id,@EventType,@EventBody)", paramItems);

                    dbcontext.Outbox.Attach(new OutBoxMessage
                    {
                        Id = item.Id,
                        EventType = item.GetType().Name,
                        EventBody = JsonConvert.SerializeObject(item)
                    });
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return base.SavedChanges(eventData, result);
        }
    }
}