using Microsoft.AspNetCore.Mvc;

namespace TestAPIServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("{location}", Name = "PostWeatherForecast")]
        public IActionResult Post([FromRoute] string location, [FromBody] PostWeatherInfo postWeatherInfo)
        {
            if (postWeatherInfo == null || string.IsNullOrEmpty(postWeatherInfo.Name))
            {
                return BadRequest("Invalid input");
            }

            // Process the postWeatherInfo object as needed
            return Ok(new PostWeatherInfo
            {
                Name = location
            });
        }
    }
}
