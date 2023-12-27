using Framework.Core;
using OrderManagement.Domain.Contract.Dto;

namespace OrderManagement.Application
{
    public class ConfirmOrderCommand : ICommand
    {
        public Guid OrderId { get; set; }

        public CommandValidationResult Validate()
        {
            return CommandValidationResult.SuccessResult();
        }
    }

    public class CreateOrderCommand : ICommand
    {
        public Guid CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; }

        public CommandValidationResult Validate()
        {
            return CommandValidationResult.SuccessResult();
        }
    }
}