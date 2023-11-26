using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.DataContract.Product
{
    public class CreateProductCommand : ICommand
    {
        public string ProductName { get; set; }
        public Guid Category { get; set; }
        public decimal Price { get; set; }
        public string CountryCode { get; set; }

        public CommandValidationResult Validate()
        {
            //TODO implement validation
            return CommandValidationResult.SuccessResult();
        }
    }
}