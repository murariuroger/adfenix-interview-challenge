namespace Adfenix.ZendeskMock.Services
{
    internal class CounterService : ICounterService
    {
        private long Counter = 0;

        public Task<string> GetCounterValueAsync()
        {
            return Task.FromResult($"{++Counter}");
        }
    }
}
