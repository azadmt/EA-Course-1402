using System.Data;
using Dapper;
namespace Catalog.ReadModel.Api
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
                new {Id=eventId});

        }
    }

}
