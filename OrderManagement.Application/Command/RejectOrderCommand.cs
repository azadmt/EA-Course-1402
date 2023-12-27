using Framework.Core;

namespace OrderManagement.Application
{
    public class RejectOrderCommand : ICommand
    {
        public Guid OrderId { get; set; }

        public CommandValidationResult Validate()
        {
            return CommandValidationResult.SuccessResult();
        }
    }
}