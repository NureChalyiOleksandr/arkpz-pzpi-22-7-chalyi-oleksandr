using SmartLightSense.Models;

namespace SmartLightSense.Dtos
{
    public record AlertCreateDto(
        int? StreetlightId,
        int? SensorId,
        string AlertType,
        string? Message 
    );

    public record AlertDto(
        int Id,
        int? StreetlightId,
        int? SensorId,
        string AlertType,
        string Message, 
        DateTime AlertDateTime,
        bool Resolved
    );

    public record AlertUpdateDto(
        int? StreetlightId,
        int? SensorId,
        string? AlertType,
        bool? Resolved,
        string? Message 
    );

    public record SendAlertNotificationDto(
        int EmployeeId,
        string Comment
    );
}
