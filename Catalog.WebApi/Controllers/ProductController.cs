using Catalog.Application;
using Catalog.Application.DataContract;
using Catalog.Application.DataContract.Product;
using Framework.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

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
            Guid CorrolationId = Guid.NewGuid();
            try
            {
                //"Processed {@Position} in {Elapsed:000} ms."
               
                Log.Information(" CorrelationId = {@CorrolationId}, Data = {@data}",
                    CorrolationId, JsonConvert.SerializeObject(createProductDto));

                //productService.CreateProductCatalog(createProductDto);
                bus.Send(createProductDto);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(" CorrelationId = {@CorrolationId}, Data = {@data}", CorrolationId, ex.ToString());
                throw;
            }
        
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