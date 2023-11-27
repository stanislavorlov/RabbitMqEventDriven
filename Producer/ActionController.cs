using MassTransit;
using Messages;
using Microsoft.AspNetCore.Mvc;
using Producer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Producer
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly ILogger<ActionController> _logger;
        private readonly IBus _bus;

        public ActionController(
            ILogger<ActionController> logger,
            IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        // GET: api/<Action>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<Action>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ActionModel value)
        {
            // ToDo: DDD business logic, validation
            // Send a command thorugh mediatr to Handler
            // Handler injects repository which Get from a Database Aggreagte Model
            // Via DDD methods performs model update
            // Aggregate model pushed events into RabbitMq

            // Move to separate service
            Uri uri = new Uri("rabbitmq://localhost/testQueue");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(new TestMessage { Id = value.Id, Name = value.Name });

            return Ok();
        }
    }
}
