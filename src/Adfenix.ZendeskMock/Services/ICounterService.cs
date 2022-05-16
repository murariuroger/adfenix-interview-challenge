namespace Adfenix.ZendeskMock.Services
{
    public interface ICounterService
    {
        Task<string> GetCounterValueAsync();
    }
}
