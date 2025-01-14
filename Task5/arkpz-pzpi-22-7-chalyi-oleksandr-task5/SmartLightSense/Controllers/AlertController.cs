using Microsoft.AspNetCore.Mvc;
using SmartLightSense.Interfaces;
using SmartLightSense.Dtos;
using SmartLightSense.Models;
using Microsoft.AspNetCore.Authorization;
using SmartLightSense.Services.EmailService;
using SmartLightSense.Repositories;

namespace SmartLightSense.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertController : ControllerBase
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IStreetlightRepository _streetlightRepository;
        private readonly ISensorRepository _sensorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;

        public AlertController(IAlertRepository alertRepository, IStreetlightRepository streetlightRepository, ISensorRepository sensorRepository, IUserRepository userRepository, IEmailSender emailSender)
        {
            _alertRepository = alertRepository;
            _streetlightRepository = streetlightRepository;
            _sensorRepository = sensorRepository;
            _userRepository = userRepository;
            _emailSender = emailSender;
        }

        [Authorize(Roles = "Sensor,Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAlert([FromBody] AlertCreateDto alertCreateDto)
        {
            if (alertCreateDto == null)
                return BadRequest("Invalid alert data");

            var newAlert = new Alert
            {
                StreetlightId = alertCreateDto.StreetlightId,
                SensorId = alertCreateDto.SensorId,
                AlertType = alertCreateDto.AlertType,
                Message = alertCreateDto.Message,
                AlertDateTime = DateTime.Now,
                Resolved = false
            };

            if (alertCreateDto.StreetlightId != null)
            {
                var streetLight = await _streetlightRepository.GetByIdAsync((int)alertCreateDto.StreetlightId);
                newAlert.Streetlight = streetLight;
            }

            if (alertCreateDto.SensorId != null)
            {
                var sensor = await _sensorRepository.GetByIdAsync((int)alertCreateDto.SensorId);
                newAlert.Sensor = sensor;
            }

            var createdAlert = await _alertRepository.CreateAsync(newAlert);

            var alertDto = new AlertDto(
                createdAlert.Id,
                createdAlert.StreetlightId,
                createdAlert.SensorId,
                createdAlert.AlertType,
                createdAlert.Message,
                createdAlert.AlertDateTime,
                createdAlert.Resolved
            );

            return CreatedAtAction(nameof(GetAlertById), new { id = createdAlert.Id }, alertDto);
        }

        [Authorize(Roles = "Technician,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlertById(int id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            if (alert == null)
                return NotFound("Alert not found");

            var alertDto = new AlertDto(
                alert.Id,
                alert.StreetlightId,
                alert.SensorId,
                alert.AlertType,
                alert.Message,
                alert.AlertDateTime,
                alert.Resolved
            );

            return Ok(alertDto);
        }

        [Authorize(Roles = "Technician,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAlerts()
        {
            var alerts = await _alertRepository.GetAllAsync();

            var alertDtos = alerts.Select(alert => new AlertDto(
                alert.Id,
                alert.StreetlightId,
                alert.SensorId,
                alert.AlertType,
                alert.Message,
                alert.AlertDateTime,
                alert.Resolved
            )).ToList();

            return Ok(alertDtos);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlert(int id, [FromBody] AlertUpdateDto alertUpdateDto)
        {
            if (alertUpdateDto == null)
                return BadRequest("Invalid alert data");

            var existingAlert = await _alertRepository.GetByIdAsync(id);
            if (existingAlert == null)
                return NotFound("Alert not found");

            if (alertUpdateDto.StreetlightId != null)
            {
                existingAlert.StreetlightId = alertUpdateDto.StreetlightId;
                var streetLight = await _streetlightRepository.GetByIdAsync((int)alertUpdateDto.StreetlightId);
                existingAlert.Streetlight = streetLight;
            }

            if (alertUpdateDto.SensorId != null)
            {
                existingAlert.SensorId = alertUpdateDto.SensorId;
                var sensor = await _sensorRepository.GetByIdAsync((int)alertUpdateDto.SensorId);
                existingAlert.Sensor = sensor;
            }

            if (alertUpdateDto.AlertType != null)
            {
                existingAlert.AlertType = alertUpdateDto.AlertType;
            }

            if (alertUpdateDto.Message != null)
            {
                existingAlert.Message = alertUpdateDto.Message;
            }

            if (alertUpdateDto.Resolved != null)
            {
                existingAlert.Resolved = (bool)alertUpdateDto.Resolved;
            }

            var updatedAlert = await _alertRepository.UpdateAsync(existingAlert);

            var updatedAlertDto = new AlertDto(
                updatedAlert.Id,
                updatedAlert.StreetlightId,
                updatedAlert.SensorId,
                updatedAlert.AlertType,
                updatedAlert.Message,
                updatedAlert.AlertDateTime,
                updatedAlert.Resolved
            );

            return Ok(updatedAlertDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(int id)
        {
            var success = await _alertRepository.DeleteAsync(id);
            if (!success)
                return NotFound("Alert not found");

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}")]
        public async Task<IActionResult> SendAlertNotification(int id, [FromBody] SendAlertNotificationDto notificationDto)
        {
            var alert = await _alertRepository.GetByIdAsync(id);

            if (alert == null)
            {
                return NotFound("Alert not found.");
            }

            var subject = $"Alert Notification: {alert.AlertType}";
            var content = $"Problem ID: {alert.Id}\n" +
                          $"Streetlight ID: {alert.StreetlightId}\n" +
                          $"Sensor ID: {alert.SensorId}\n" +
                          $"Alert Message: {alert.Message}\n" +
                          $"Alert Date/Time: {alert.AlertDateTime}\n" +
                          $"Resolved: {alert.Resolved}\n\n" +
                          $"Comment: {notificationDto.Comment}";

            var technician = await _userRepository.GetByIdAsync(notificationDto.EmployeeId);
            string email = "";

            if (technician == null || technician.Role != "Technician")
            {
                return BadRequest("User must be technician!");
            }

            email = technician.Email;

            if (string.IsNullOrEmpty(email))
            {
                return NotFound("Employee not found or does not have the Technician role.");
            }

            var message = new Message(new List<string> { email }, subject, content);

            await _emailSender.SendEmail(message);

            return Ok("Notification sent successfully.");
        }

    }
}
