using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceRegistryController : ControllerBase
    {
        [HttpPost(Name = "RegisterService")]
        public IActionResult RegisterService(ServiceRegistryModel model)
        {
            ServiceRegistry.RegisterNewServcie(model);
            return Ok();
        }
    }
}