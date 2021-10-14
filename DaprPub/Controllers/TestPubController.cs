using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapr.Client;

namespace DaprPub.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestPubController : ControllerBase
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<TestPubController> _logger;
        public TestPubController(DaprClient daprClient, ILogger<TestPubController> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
        }

        [HttpGet("pub")]
        public async Task<ActionResult> Pub()
        {
            var data = new OrderData
            {
                orderId = "123456",
                productId = "67890",
                amount = 2
            };

            //using var daprClient = new DaprClientBuilder().UseHttpEndpoint("http://localhost:3501").Build();

            //await daprClient.PublishEventAsync<WeatherForecast>("pubsub", "test_topic", data);

            await _daprClient.PublishEventAsync<OrderData>("pubsub", "newOrder3", data);

            _logger.LogInformation("published:"+data.productId);

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
