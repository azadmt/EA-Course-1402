using Framework.Core;
using Framework.Domain;
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
            var outbox = eventData
                   .Context
                   .ChangeTracker
                   .Entries<IAggregateRoot>()
                   .Select(entry => entry.Entity)
                       .SelectMany(entity =>
                       {
                           var domainEvents = entity.GetChanges();
                           return domainEvents;
                       });

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"INSERT INTO outbox (Id,EventType,EventBody) VALUES ");
            foreach (var item in outbox)
            {
                sb.AppendLine($"({item.Id},{item.GetType()},{JsonConvert.SerializeObject(item)},'false')");
            }
            eventData
           .Context
           .Database
           .ExecuteSqlRaw(sb.ToString()); ;
            return base.SavedChanges(eventData, result);
        }
    }
}