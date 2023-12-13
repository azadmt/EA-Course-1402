using Microsoft.AspNetCore.Mvc;

namespace Catalog.ReadModel.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCatalogController : ControllerBase
    {
        public ProductCatalogController()
        {
        }

        [HttpGet("id")]
        public IActionResult Get(Guid id)
        {
            return Ok();
        }
    }
}