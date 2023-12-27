using Framework.Core;
using OrderManagement.Domain.Contract.Dto;

namespace OrderManagement.Application
{
    public class AddNewItemsToOrderCommand : ICommand
    {
        public Guid OrderId { get; set; }
        public List<OrderItemDto> Items { get; set; }

        public CommandValidationResult Validate()
        {
            return CommandValidationResult.SuccessResult();
        }
    }
}