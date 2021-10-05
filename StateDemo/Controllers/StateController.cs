using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapr.Client;

namespace StateDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly DaprClient _daprClient;
        public StateController(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result= await _daprClient.GetStateAsync<string>("statestore", "guid");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _daprClient.SaveStateAsync<string>("statestore", "guid", Guid.NewGuid().ToString(),new StateOptions { Consistency=ConsistencyMode.Strong });
            return Ok("done");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            await _daprClient.DeleteStateAsync("statestore", "guid");
            return Ok("done");
        }

        /// <summary>
        /// 获取一个值和etag
        /// </summary>
        /// <returns></returns>
        [HttpGet("withetag")]
        public async Task<ActionResult> GetWithEtag()
        {
            var (value, etag) = await _daprClient.GetStateAndETagAsync<string>("statestore", "guid");
            return Ok($"value is {value}, etag is {etag}");
        }

    }
}
