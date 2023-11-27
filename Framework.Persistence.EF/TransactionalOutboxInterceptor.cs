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
            var dbcontext = (eventData.Context as ApplicationDbContext);
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
            sb.Append($"INSERT INTO outbox (Id,EventType,EventBody) VALUES ");
            var paramItems = new List<SqlParameter>();
            for (int i = 0; i < outbox.Count; i++)
            {
                //paramItems[i] = new object[]
                //    {
                paramItems.Add(new SqlParameter($"@Id{i}", outbox[i].Id.ToString()));
                paramItems.Add(new SqlParameter($"@EventType{i}", outbox[i].GetType().Name));
                paramItems.Add(new SqlParameter($"@EventBody{i}", JsonConvert.SerializeObject(outbox[i])));
                //  };
                //  sb.Append($" ('{item.Id}','{item.GetType()}','{JsonConvert.SerializeObject(item)}')");
                sb.AppendLine($" (@Id{i},@EventType{i},@EventBody{i}) ");
                if (i != outbox.Count - 1)
                    sb.Append($" , ");
            }

            eventData
                 .Context
                     .Database
                     .ExecuteSqlRaw(sb.ToString(), paramItems.ToArray());
            //    .ExecuteSqlRaw("INSERT INTO outbox (Id,EventType,EventBody) VALUES (@Id,@EventType,@EventBody)", paramItems);

            return base.SavedChanges(eventData, result);
        }
    }
}