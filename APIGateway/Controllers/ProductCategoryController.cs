using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Retry;
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
        public IActionResult CreateProductCategory(CreateProductCategoryModel model)
        {
            //  var serviceurl = ServiceRegistry.GetService("ProductCategory");
            var healthResult = ServiceRegistry.CheckHealth("http://localhost:5095");
            //var client = new RestClient("http://localhost:5095");
            var client = new RestClient("http://localhost:5095");
            var request = new RestRequest("/api/ProductCategory");
            request.AddBody(model);
            var result = client.Post(request);
            return Ok();
        }

        private static RetryPolicy<RestResponse> GetRetryPolicy()
        {
            return Policy
                .HandleResult<RestResponse>(
                    r =>
                        r.Request.Method == Method.Get
                        && new List<int> { 500, 502, 503, 504 }
                              .Contains((int)r.StatusCode))
                .OrResult(
                    r =>
                        r.Request.Method != Method.Get
                        && new List<int> { 500, 502, 503 }
                              .Contains((int)r.StatusCode))
                .WaitAndRetryAsync(4,
                    attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));
        }
    }

    public class CreateProductCategoryModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}