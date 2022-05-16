using Adfenix.Visualiser.Models;

namespace Adfenix.Visualiser.Services
{
    public interface ISeriesService
    {
        Task<IEnumerable<Series>> GetSeriesAsync();
        Task AddSeriesAsync(Series series);
    }
}
