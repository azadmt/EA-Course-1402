using OrderManagement.Domain.Order;
using OrderManagement.Domain.Order.State;

namespace OrderManagement.Test
{
    public class OrderTest
    {
        [Fact]
        public void When_Created_Must_be_In_Pending_State()
        {
            var order = OrderAggregate.CreateOrder(Guid.NewGuid(), Guid.NewGuid(), new List<Domain.Contract.Dto.OrderItemDto>());

            Assert.Equal(order.State.GetType(), typeof(PendingState));
        }


        [Fact]
        public void can_not_deliver_when_order_is_in_pending_state()
        {
            var order = OrderAggregate.CreateOrder(Guid.NewGuid(), Guid.NewGuid(), new List<Domain.Contract.Dto.OrderItemDto>());
            Exception expectedException = new Exception();
            try
            {
                order.Deliver();

            }
            catch (Exception ex)
            {
                expectedException = ex;
            }
            Assert.Equal(expectedException.GetType(), typeof(NotSupportedException));
        }
    }
}