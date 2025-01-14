using SmartLightSense.Models;

namespace SmartLightSense.Interfaces;

public interface IStreetlightRepository
{
    Task<Streetlight> CreateAsync(Streetlight streetlight);
    Task<Streetlight> UpdateAsync(Streetlight streetlight);
    Task<bool> DeleteAsync(int streetlightId);
    Task<Streetlight> GetByIdAsync(int streetlightId);
    Task<List<Streetlight>> GetAllAsync();
    Task UpdateBrightnessAsync(int streetlightId, int brightnessLevel);
    Task<List<Streetlight>> GetBySectorIdAsync(int sectorId);
}
