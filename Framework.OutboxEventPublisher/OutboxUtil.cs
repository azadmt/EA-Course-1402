using System.Data;
using Dapper;

namespace Framework.OutboxEventPublisher
{
    public class Publisher
    {
        public Publisher()
        { }

        public void PublishOutboxItems()
        {
            //  using connection=new SqlConnection
        }
    }

    public static class OutboxUtil
    {
        public static List<OutboxItem> GetOutboxes(this IDbConnection dbConnection)
        {
            return dbConnection.Query<OutboxItem>("SELECT * FROM Outbox Where PublishedAt IS NULL").ToList();
        }

        public static void UpdatePublishedDate(this IDbConnection dbConnection, IEnumerable<long> ItemsId)
        {
            dbConnection.Execute("UPDATE Outbox Set PaublishedAt=@publishedAt Where Id @ids",
                  param: new { publishedAt = DateTime.Now, ids = ItemsId });
        }
    }

    public class OutboxItem
    {
        public string EventType { get; set; }
        public string EventBody { get; set; }
        public Guid EventId { get; set; }
        public long Id { get; set; }
    }
}