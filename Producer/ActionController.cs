using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Producer
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly ILogger<ActionController> _logger;

        public ActionController(ILogger<ActionController> logger)
        {
            _logger = logger;
        }

        // GET: api/<Action>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<Action>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            // ToDo: DDD business logic, validation
            // Send a command thorugh mediatr to Handler
            // Handler injects repository which Get from a Database Aggreagte Model
            // Via DDD methods performs model update
            // Aggregate model pushed events into RabbitMq

            // Move to separate service
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "producerQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = $"Producer: DDD event from Action: {value}";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                routingKey: "hello",
                                basicProperties: null,
                                body: body);

            _logger.LogDebug("Post action event sent ");

            return Ok();
        }
    }
}
