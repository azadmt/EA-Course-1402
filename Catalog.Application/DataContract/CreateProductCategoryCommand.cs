using Framework.Core;

namespace Catalog.Application.DataContract
{
    public class CreateProductCategoryCommand : ICommand
    {
        public string Name{ get; set; }
        public string Code{ get; set; }

        public bool IsValid()
        {
          return  true;
        }
    }
}