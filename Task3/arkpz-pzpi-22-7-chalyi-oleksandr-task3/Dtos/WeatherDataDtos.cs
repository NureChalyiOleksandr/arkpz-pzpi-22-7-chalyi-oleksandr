using SmartLightSense.Models;

namespace SmartLightSense.Dtos
{
    public record WeatherDataCreateDto(
        double Visibility
    );

    public record WeatherDataDto(
        int Id,
        DateTime Date,
        double Visibility
    );

    public record WeatherDataUpdateDto(
        double? Visibility
    );
}
