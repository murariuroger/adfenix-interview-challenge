using Adfenix.Visualiser.Models;
using Adfenix.Visualiser.Services;
using Adfenix.VisualiserMock.Models.Requests;
using Adfenix.VisualiserMock.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Adfenix.Visualiser.Controllers
{
    [ApiController]
    [Route("api/v1/series")]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesService _seriesService;

        public SeriesController(ISeriesService seriesService)
        {
            _seriesService = seriesService ?? throw new ArgumentNullException(nameof(seriesService));
        }

        [HttpGet]
        public async Task<IActionResult> GetSeries()
        {
            var series = await _seriesService.GetSeriesAsync();

            return Ok(new SeriesResponse
            {
                Series = series
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddSeries(AddSeriesRequest request)
        {
            foreach (var item in request.Series)
            {
                await _seriesService.AddSeriesAsync(item);
            }

            return Ok();
        }
    }
}