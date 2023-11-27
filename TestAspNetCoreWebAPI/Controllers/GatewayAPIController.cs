using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Security.Authentication;

namespace TestAspNetCoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GatewayAPIController : ControllerBase
    {
        private readonly ILogger _logger;

        private string? _consumerApiURI = string.Empty;
        private string? _producerApiURI = string.Empty;

        public GatewayAPIController(IConfiguration configuration)
        {
            _consumerApiURI = configuration.GetValue<string>("ConsumerAPIHost");
            _producerApiURI = configuration.GetValue<string>("ProducerAPIHost");
            //_logger = logger;
        }

        [HttpGet(Name = "GetConsumerData")]
        public async Task<IActionResult> Get()
        {
            HttpClient httpClient = new HttpClient();

            var httpResponse = await httpClient.GetAsync(_consumerApiURI + "/api/values");

            if (httpResponse.IsSuccessStatusCode)
            {
                return Ok(await httpResponse.Content.ReadAsStringAsync());
            }

            return BadRequest();
        }

        [HttpPost(Name = "CreateData")]
        public async Task<IActionResult> Post()
        {
            var handler = new HttpClientHandler()
            {
                SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls,
            };

            HttpClient httpClient = new HttpClient(handler);

            var httpResponse = await httpClient.PostAsJsonAsync(_producerApiURI + "/api/action", $"Hello world {DateTime.UtcNow}");

            if (httpResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Succesfully created data");

                return Ok();
            }

            Console.WriteLine("Failed to create data");

            return BadRequest();
        }
    }
}
