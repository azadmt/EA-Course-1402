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
            sb.Append($"INSERT INTO outbox (EventId,EventType,EventBody) VALUES ");
            var paramItems = new List<SqlParameter>();
            for (int i = 0; i < outbox.Count; i++)
            {
                paramItems.Add(new SqlParameter($"@EventId{i}", outbox[i].Id.ToString()));
                paramItems.Add(new SqlParameter($"@EventType{i}", outbox[i].GetType().AssemblyQualifiedName));
                paramItems.Add(new SqlParameter($"@EventBody{i}", JsonConvert.SerializeObject(outbox[i])));

                sb.AppendLine($" (@EventId{i},@EventType{i},@EventBody{i}) ");
                if (i != outbox.Count - 1)
                    sb.Append($" , ");
            }

            dbcontext
                     .Database
                     .ExecuteSqlRaw(sb.ToString(), paramItems.ToArray());

            return base.SavedChanges(eventData, result);
        }
    }

    public static class OutboxUtil
    {
        public static void CreateTableIfNotExist()
        {
            var sql = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Outbox]') AND type in (N'U'))
                    BEGIN
                        SET ANSI_NULLS ON
                        GO

                        SET QUOTED_IDENTIFIER ON
                        GO

                        CREATE TABLE [dbo].[Outbox](
	                        [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                        [EventType] [nvarchar](500) NOT NULL,
	                        [EventBody] [nvarchar](max) NOT NULL,
	                        [PublishedAt] [datetime] NULL,
	                        [Created] [datetime] NOT NULL,
	                        [EventId] [uniqueidentifier] NOT NULL
                        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                        GO
                       ALTER TABLE [dbo].[Outbox] ADD  CONSTRAINT [DF_Outbox_Created]  DEFAULT (getdate()) FOR [Created]

                    END";
        }
    }
}