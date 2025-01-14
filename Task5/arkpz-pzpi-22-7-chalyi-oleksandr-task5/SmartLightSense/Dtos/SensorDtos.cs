﻿using SmartLightSense.Models;

namespace SmartLightSense.Dtos
{
    public record SensorCreateDto(
        string SensorType,
        string Status,
        int Data,
        int StreetlightId
    );

    public record SensorDto(
        int Id,
        string SensorType,
        DateTime InstallationDate,
        string Status,
        int Data,
        DateTime LastUpdate,
        int StreetlightId
    );

    public record SensorGetDto(
        int Id,
        string SensorType,
        DateTime InstallationDate,
        string Status,
        int Data,
        DateTime LastUpdate
    );

    public record SensorUpdateDto(
        string? SensorType,
        string? Status,
        int? Data,
        DateTime? LastUpdate,
        int? StreetlightId
    );
}
