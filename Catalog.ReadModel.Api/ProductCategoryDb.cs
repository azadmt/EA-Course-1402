using System.Data;
using Catalog.ReadModel.Api.DataModel;
using Dapper;
namespace Catalog.ReadModel.Api
{
    public class ProductCategoryDbManager
    {
        private readonly IDbConnection _dbConnection;

        public ProductCategoryDbManager(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Insert(ProductCategory productCategory)
        {
            _dbConnection
                .Execute("INSERT INTO ProductCategories (Id,Name,Code) VALUES (@Id,@Name,@Code) ",
                productCategory);

        }
    }

}
