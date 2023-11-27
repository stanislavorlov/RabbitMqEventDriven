namespace Consumer.Services
{
    public class DataService
    {
        private List<ConsumedMessage> messages = new List<ConsumedMessage>();

        public DataService() { }

        public void AddItem(string message)
        {
            messages.Add(new ConsumedMessage { Message = message, IsRead = false });
        }

        public List<string?> GetItems()
        {
            var items = messages.Where(m => !m.IsRead).ToList();

            items.ForEach(item => item.IsRead = true);

            return items.Select(i => i.Message).ToList();
        }

        private class ConsumedMessage
        {
            public bool IsRead { get; set; }

            public string? Message { get; set; }
        }
    }
}
