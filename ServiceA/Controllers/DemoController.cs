using Dapr.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        // 通过HttpClient调用serviceb
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            using var httpClient = DaprClient.CreateInvokeHttpClient("serviceb", "http://localhost:3511");
            var result = await httpClient.GetAsync("/WeatherForecast");
            //using var httpClient = DaprClient.CreateInvokeHttpClient();
            //var result = await httpClient.GetAsync("http://serviceb/WeatherForecast");
            var resultContent = string.Format("result is {0} {1}", result.StatusCode, await result.Content.ReadAsStringAsync());
            return Ok(resultContent);
        }

        // 通过DaprClient调用serviceb
        [HttpGet("get2")]
        public async Task<ActionResult> Get2()
        {
            using var daprClient = new DaprClientBuilder().UseHttpEndpoint("http://localhost:3511").Build();
           
            var result = await daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(HttpMethod.Get,"serviceb", "WeatherForecast");
            return Ok(result);
        }
    }
}
