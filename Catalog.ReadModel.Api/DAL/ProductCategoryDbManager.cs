using System.Data;
using Catalog.ReadModel.Api.DataModel;
using Dapper;

namespace Catalog.ReadModel.Api.DAL
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

    public class ProductCatalogDbManager
    {
        private readonly IDbConnection _dbConnection;

        public ProductCatalogDbManager(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Insert(ProductCatalog model)
        {
            _dbConnection
                .Execute("INSERT INTO ProductCatalogs (Id,CategoryId,Code,Price,IsActive) VALUES (@Id,@CategoryId,@Code,@Price,@IsActive)",
                model);
        }

        public ProductCatalog Get(Guid pId)
        {
            return _dbConnection
                  .QueryFirst<ProductCatalog>("SELECT  * FROM  ProductCatalogs WHERE Id=@Id",
                  new { Id = pId });
        }
    }
}