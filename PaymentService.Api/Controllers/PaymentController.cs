using Microsoft.AspNetCore.Mvc;
using PaymentService.Api.Model;

namespace PaymentService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        public PaymentController()
        {
        }

        [HttpPost]
        public IActionResult PaymentRequest(PaymentRequest paymentRequest)
        {
            return Ok();
        }
    }
}