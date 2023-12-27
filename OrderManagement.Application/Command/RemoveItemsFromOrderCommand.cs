using Framework.Core;

namespace OrderManagement.Application
{
    public class RemoveItemsFromOrderCommand : ICommand
    {
        public Guid OrderId { get; set; }
        public List<Guid> ItemsId { get; set; }

        public CommandValidationResult Validate()
        {
            return CommandValidationResult.SuccessResult();
        }
    }
}