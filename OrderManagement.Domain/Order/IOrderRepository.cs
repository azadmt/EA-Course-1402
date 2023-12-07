namespace OrderManagement.Domain.Order
{
    public interface IOrderRepository
    {
        OrderAggregate Get(Guid id);

        void Save(OrderAggregate aggregate);

        void Update(OrderAggregate aggregate);
    }
}