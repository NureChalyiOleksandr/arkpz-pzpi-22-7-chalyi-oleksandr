using Microsoft.AspNetCore.Mvc;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;
using SmartLightSense.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace SmartLightSense.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherDataController : ControllerBase
    {
        private readonly IWeatherDataRepository _weatherDataRepository;

        public WeatherDataController(IWeatherDataRepository weatherDataRepository)
        {
            _weatherDataRepository = weatherDataRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<WeatherDataDto>>> GetAll()
        {
            var weatherDataList = await _weatherDataRepository.GetAllAsync();

            return Ok(weatherDataList);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherDataDto>> GetById(int id)
        {
            var weatherData = await _weatherDataRepository.GetByIdAsync(id);
            if (weatherData == null)
            {
                return NotFound();
            }

            return Ok(weatherData);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<WeatherDataDto>> Create([FromBody] WeatherDataCreateDto weatherDataCreateDto)
        {
            var weatherData = new WeatherData
            {
                Date = DateTime.Now,
                Visibility = weatherDataCreateDto.Visibility,
            };

            var createdWeatherData = await _weatherDataRepository.CreateAsync(weatherData);

            return Ok(createdWeatherData);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<WeatherDataDto>> Update(int id, [FromBody] WeatherDataUpdateDto weatherDataUpdateDto)
        {
            var weatherData = await _weatherDataRepository.GetByIdAsync(id);
            if (weatherData == null)
            {
                return NotFound();
            }

            if (weatherDataUpdateDto.Visibility != null)
            {
                weatherData.Visibility = (double)weatherDataUpdateDto.Visibility;
            }
      
            var updatedWeatherData = await _weatherDataRepository.UpdateAsync(weatherData);

            return Ok(updatedWeatherData);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _weatherDataRepository.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
