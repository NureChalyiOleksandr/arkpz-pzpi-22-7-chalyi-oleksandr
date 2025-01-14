using SmartLightSense.Models;

namespace SmartLightSense.Interfaces;

public interface IEnergyUsageRepository
{
    Task<EnergyUsage> CreateAsync(EnergyUsage energyUsage);
    Task<EnergyUsage> UpdateAsync(EnergyUsage energyUsage);
    Task<bool> DeleteAsync(int energyUsageId);
    Task<EnergyUsage> GetByIdAsync(int energyUsageId);
    Task<List<EnergyUsage>> GetAllAsync();
    Task<EnergyUsage?> GetByDateAndStreetlightIdAsync(int streetlightId, DateTime date);
    Task<EnergyUsage?> GetLatestByStreetlightIdAsync(int streetlightId);
    Task<List<EnergyUsage>> GetLatestForAllStreetlightsAsync();
    Task<List<EnergyUsage>> GetByDateForAllStreetlightsAsync(DateTime date);
}
