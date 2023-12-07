using Framework.Core;

namespace OrderManagement.Application
{
    public class CreateOrderCommand : ICommand
    {
        public Guid CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; }

        public CommandValidationResult Validate()
        {
            return CommandValidationResult.SuccessResult();
        }
    }

    public class AddNewItemsToOrderCommand : ICommand
    {
        public Guid OrderId { get; set; }
        public List<OrderItemDto> Items { get; set; }

        public CommandValidationResult Validate()
        {
            return CommandValidationResult.SuccessResult();
        }
    }

    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}