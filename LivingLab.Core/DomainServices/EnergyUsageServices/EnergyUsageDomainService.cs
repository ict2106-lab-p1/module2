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
    
    public Task<List<EnergyUsageDTO>> GetEnergyUsage(EnergyUsageFilterDTO filter)
    {
        // TODO: Call Energy Usage repo to get filtered energy usage
        // TODO: Map to DTO
        throw new NotImplementedException();
    }

    public Task SetLabEnergyBenchmark(EnergyBenchmarkDTO benchmark)
    {
        // TODO: Call Lab repo to set energy benchmark
        throw new NotImplementedException();
    }
}
