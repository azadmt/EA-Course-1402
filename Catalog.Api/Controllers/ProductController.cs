using Catalog.Application;
using Catalog.Application.DataContract;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService productService;

        public ProductController(ProductService  productService)
        {
            this.productService = productService;
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto )
        {
            return View();
        }
    }
}
