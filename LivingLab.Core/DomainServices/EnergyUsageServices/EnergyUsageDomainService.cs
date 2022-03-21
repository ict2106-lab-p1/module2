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

        var benchmark = await _labRepository.GetLabEnergyBenchmark(filter.Lab.LabId);
        
        var dto = new EnergyUsageDTO
        {
            Logs = logs,
            LabId = filter.Lab.LabId,
            EnergyUsageBenchmark = benchmark
        };
        return dto;
    }

    /// <summary>
    /// Call Lab repo to set the current lab total energy benchmark
    /// </summary>
    /// <param name="benchmark">Benchmark DTO object</param>
    public Task SetLabEnergyBenchmark(EnergyBenchmarkDTO benchmark)
    {
        return _labRepository.SetLabEnergyBenchmark(benchmark.Lab.LabId, benchmark.EnergyUsageBenchmark);
    }
}
