using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockControllerController : ControllerBase
    {
        private readonly ILogger<StockControllerController> _logger;

        public StockControllerController(ILogger<StockControllerController> logger)
        {
            _logger = logger;
        }
    }
}