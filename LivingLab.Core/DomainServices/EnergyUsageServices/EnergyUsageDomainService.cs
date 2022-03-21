using LivingLab.Core.Entities;
using LivingLab.Core.Entities.DTO.EnergyUsageDTOs;
using LivingLab.Core.Interfaces.Repositories;
using LivingLab.Core.Interfaces.Services.EnergyUsageInterfaces;

namespace LivingLab.Core.DomainServices.EnergyUsageServices;

/// <remarks>
/// Author: Team P1-1
/// </remarks>
public class EnergyUsageDomainService : IEnergyUsageDomainService
{
    private readonly ILabRepository _labRepository;
    private readonly IEnergyUsageRepository _energyUsageRepository;
    
    public EnergyUsageDomainService(ILabRepository labRepository, IEnergyUsageRepository energyUsageRepository)
    {
        _labRepository = labRepository;
        _energyUsageRepository = energyUsageRepository;
    }
    
    /// <summary>
    /// 1. Call Energy Usage repo to get filtered energy usage data
    /// 2. Map logs to DTO
    /// </summary>
    /// <param name="filter">Filter DTO object</param>
    /// <returns>EnergyUsageDTO</returns>
    public async Task<EnergyUsageDTO> GetEnergyUsage(EnergyUsageFilterDTO filter)
    {
        var logs = await _energyUsageRepository
            .GetDeviceEnergyUsageByLabAndDate(filter.Lab.LabId, filter.Start, filter.End);

        var lab = await _labRepository.GetByIdAsync(filter.Lab.LabId);
        
        var dto = new EnergyUsageDTO
        {
            Logs = logs,
            Lab = lab
        };
        return dto;
    }

    public Task<Lab> GetLabEnergyBenchmark(int labId)
    {
        return _labRepository.GetByIdAsync(labId);
    }

    /// <summary>
    /// Call Lab repo to set the current lab total energy benchmark
    /// </summary>
    /// <param name="benchmark">Benchmark DTO object</param>
    public Task SetLabEnergyBenchmark(Lab lab)
    {
        return _labRepository.SetLabEnergyBenchmark(lab.LabId, lab.EnergyUsageBenchmark!.Value);
    }
}
