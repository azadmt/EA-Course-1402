using Catalog.Application.DataContract;
using Framework.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ICommandBus bus;

        public ProductCategoryController(ICommandBus bus)
        {
            this.bus = bus;
        }

        [HttpPost]
        public IActionResult CreateProduct( CreateProductCategoryCommand command)
        {            
            bus.Send(command);
            return Ok();
        }

    }
}
