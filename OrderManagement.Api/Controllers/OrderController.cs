using Framework.Core;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application;

namespace OrderManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public OrderController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        [HttpPost(nameof(CreateOrder))]
        public IActionResult CreateOrder(CreateOrderCommand command)
        {
            _commandBus.Send(command);
            return Ok();
        }

        [HttpPost(nameof(AddNewItemsToOrder))]
        public IActionResult AddNewItemsToOrder(AddNewItemsToOrderCommand command)
        {
            _commandBus.Send(command);
            return Ok();
        }
    }
}