using Framework.Core;

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
}