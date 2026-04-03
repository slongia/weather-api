using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace MyApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public WeatherController(ILogger<WeatherController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _baseUrl = configuration["WeatherApi:BaseUrl"] ?? string.Empty;
            _apiKey = configuration["WeatherApi:ApiKey"] ?? string.Empty;
        }

        [HttpGet("json")]
        public async Task<ActionResult<WeatherResponse>> Get([FromQuery] string location = "Bothell")
        {
            _logger.LogInformation("Fetching weather for location: {Location}", location);
            var client = _httpClientFactory.CreateClient();
            var forecast = await client.GetFromJsonAsync<WeatherResponse>(
                $"{_baseUrl}/current.json?key={_apiKey}&q={Uri.EscapeDataString(location)}");
            _logger.LogInformation("Received weather data: {@Forecast}", forecast);
            return Ok(forecast);
        }
    }
}