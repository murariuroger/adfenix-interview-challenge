namespace Adfenix.CampaignServersMock.Services
{
    internal class CounterService : ICounterService
    {
        private readonly Random _random = new Random();
        private Dictionary<string, long> _counters = new();
        public Task<long> GetCounterValueAsync(string server)
        {
            if (_counters.ContainsKey(server))
            {
                _counters[server] += _random.Next(1, 11);
                return Task.FromResult(_counters[server]);
            }

            _counters[server] = 0;
            return Task.FromResult(_counters[server]);
        }
    }
}
