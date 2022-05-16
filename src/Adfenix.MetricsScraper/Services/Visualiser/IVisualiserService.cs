using Adfenix.MetricsScraper.Models.Visualiser.Requests;
using Adfenix.MetricsScraper.Models.Visualiser.Responses;

namespace Adfenix.MetricsScraper.Services
{
    internal interface IVisualiserService
    {
        Task<SeriesResponse> GetSeries();
        Task AddSeries(AddSeriesRequest request);
    }
}
