using Framework.Core;

namespace Catalog.Application.DataContract.Product
{
    public class ActiveProductCommand : ICommand
    {
        public Guid Id { get; set; }

        public CommandValidationResult Validate()
        {
            //TODO implement validation
            return CommandValidationResult.SuccessResult();
        }
    }
}