using APIGateway.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace APIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        public ProductCategoryController()
        {
        }

        [HttpPost]
        //  [LogActionFilter]
        [MyCustomAuthorizeFilter(Operation = "CreateProductCategory")]
        public IActionResult CreateProductCategory(CreateProductCategoryModel model)
        {
            var client = new RestClient("http://localhost:5095");
            var request = new RestRequest("/api/ProductCategory");
            request.AddBody(model);
            var result = client.Post(request);
            return Ok();
        }
    }

    public class CreateProductCategoryModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}