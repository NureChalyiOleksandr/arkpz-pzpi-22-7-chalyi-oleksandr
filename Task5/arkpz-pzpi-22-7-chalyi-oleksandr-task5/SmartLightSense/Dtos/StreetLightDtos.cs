using SmartLightSense.Models;

namespace SmartLightSense.Dtos
{
    public record StreetlightCreateDto(
        int SectorId,
        string Type,
        string Status,
        int BrightnessLevel
    );

    public record StreetlightDto(
        int Id,
        int SectorId,
        string Type,
        DateTime InstallationDate,
        string Status,
        int BrightnessLevel,
        DateTime LastMaintenanceDate
    );

    public record StreetlightGetDto(
        int Id,
        int SectorId,
        string Type,
        DateTime InstallationDate,
        string Status,
        int BrightnessLevel,
        DateTime LastMaintenanceDate,
        IEnumerable<SensorGetDto>? Sensors
    );

    public record StreetlightGetAllDto(
        int Id,
        int SectorId,
        string Type,
        DateTime InstallationDate,
        string Status,
        int BrightnessLevel,
        DateTime LastMaintenanceDate
    );

    public record StreetlightUpdateDto(
        int? SectorId,
        string? Type,
        string? Status,
        int? BrightnessLevel,
        DateTime? LastMaintenanceDate
    );
}
