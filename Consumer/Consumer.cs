using Consumer.Services;
using MassTransit;
using Messages;

namespace Consumer
{
    public class Consumer : IConsumer<TestMessage>
    {
        private readonly DataService _dataService;

        public Consumer(DataService dataService)
        {
            _dataService = dataService;
        }

        public Task Consume(ConsumeContext<TestMessage> context)
        {
            var data = context.Message;

            _dataService.AddItem(data);

            return Task.CompletedTask;
        }
    }
}
