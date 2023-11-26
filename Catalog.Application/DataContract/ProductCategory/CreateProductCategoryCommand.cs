using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.DataContract.ProductCategory
{
    public class CreateProductCategoryCommand : ICommand
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public CommandValidationResult Validate()
        {
            return CommandValidationResult.SuccessResult();
        }
    }
}