using System.Data;
using Dapper;

namespace Catalog.ReadModel.Api.DAL
{
    public class InboxDbManager
    {
        private readonly IDbConnection dbConnection;

        public InboxDbManager(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public void Insert(Guid eventId)
        {
            dbConnection
                .Execute("INSERT INTO Inbox (Id) VALUES (@Id) ",
                new { Id = eventId });
        }

        public bool Exist(Guid eventId)
        {
            var result = dbConnection
                   .Query<int>("SELECT 1 FROM Inbox WHERE Id=@Id ",
                   new { Id = eventId }).First();

            return result == 1;
        }
    }
}