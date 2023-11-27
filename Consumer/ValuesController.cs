using Consumer.Services;
using Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Consumer
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly DataService _dataService;

        public ValuesController(
            ILogger<ValuesController> logger,
            DataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        // GET: api/<Action>
        [HttpGet]
        public IEnumerable<TestMessage?> Get()
        {
            return _dataService.GetItems();
        }
    }
}
