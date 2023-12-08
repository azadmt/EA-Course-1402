using Catalog.Domain.Contract;
using MassTransit;
using System.Transactions;

namespace Catalog.ReadModel.Api.EventHandlers
{
    public class ProductCategoryCreatedEventHandler : IConsumer<ProductCategoryCreatedEvent>
    {
        private readonly ProductCategoryDbManager productCategoryDb;
        private readonly InboxDbManager inboxDbManager;

        public ProductCategoryCreatedEventHandler(ProductCategoryDbManager productCategoryDb , InboxDbManager inboxDbManager1)
        {
            this.productCategoryDb = productCategoryDb;
            this.inboxDbManager = inboxDbManager1;
        }
        public async Task Consume(ConsumeContext<ProductCategoryCreatedEvent> context)
        {
            // if( context.Message.Id) check handeld?
            using TransactionScope trx=new TransactionScope();
            productCategoryDb
                .Insert(new DataModel.ProductCategory { Id = context.Message.ProductCategoryId, Name = context.Message.Name, Code = context.Message.Code });
            inboxDbManager.Insert(context.Message.Id);
            trx.Complete();
            Console.WriteLine($"RECIVE :{context.Message.Code}");
            //update inbox
        }
    }
}
