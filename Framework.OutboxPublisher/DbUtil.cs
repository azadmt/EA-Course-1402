using Dapper;
using System.Data;

namespace Framework.OutboxPublisher
{
    public static class DbUtil
    {
        public static List<OutboxItem> GetOutboxes(this IDbConnection dbConnection)
        {
            return dbConnection.Query<OutboxItem>("SELECT * FROM Outbox Where PublishedAt IS NULL").ToList();
        }

        public static void UpdatePublishedDate(this IDbConnection dbConnection, IEnumerable<long> ItemsId)
        {
            dbConnection.Execute("UPDATE Outbox Set PublishedAt=@publishedAt Where Id IN @ids",
                  param: new { publishedAt = DateTime.Now, ids = ItemsId });
        }
    }
}