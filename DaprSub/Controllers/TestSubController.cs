using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DaprSub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestSubController : ControllerBase
    {
        private readonly DaprClient _daprClient;

        private readonly ILogger<TestSubController> _logger;
        public TestSubController(DaprClient daprClient, ILogger<TestSubController> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
        }

        [Topic("pubsub", "newOrder3")]
        [HttpPost("/CreateOrder")]
        public async Task<ActionResult> CreateOrder(OrderData order)
        {
            _logger.LogInformation("newOrder:" + order.productId);

            return Ok();
        }



    }
    public class OrderData
    {
        public string orderId { get; set; }

        public string productId { get; set; }

        public int amount { get; set; }
    }
}
