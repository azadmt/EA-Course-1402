using Catalog.Application.DataContract.ProductCategory;
using Framework.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ICommandBus _bus;

        public ProductCategoryController(ICommandBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public IActionResult Create(CreateProductCategoryCommand command)
        {
            _bus.Send(command);
            return Ok();
        }
    }
}