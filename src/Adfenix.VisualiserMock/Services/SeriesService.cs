using Adfenix.Visualiser.Models;

namespace Adfenix.Visualiser.Services
{
    internal class SeriesService : ISeriesService
    {
        private List<Series> _series = new();

        public Task AddSeriesAsync(Series series)
        {
            _series.Add(series);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Series>> GetSeriesAsync()
        {
            return Task.FromResult(_series.AsEnumerable());
        }
    }
}
