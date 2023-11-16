using Catalog.Application;
using Catalog.Application.DataContract;
using Framework.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICommandBus bus;

        //private readonly ProductService productService;

        //public ProductController(ProductService productService)
        //{
        //    this.productService = productService;
        //}

        public ProductController(ICommandBus bus)
        {
            this.bus = bus;
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductCommand createProductDto)
        {
            //productService.CreateProductCatalog(createProductDto);
            bus.Send(createProductDto);
            return Ok();
        }

        [HttpPost("DeActive")]
        public IActionResult Deactive(Guid productId)
        {
            //productService.DeActiveProduct(productId);
            return Ok();
        }

        [HttpPost("Active")]
        public IActionResult Active(Guid productId)
        {
           // productService.ActiveProduct(productId);
            return Ok();
        }
    }
}
