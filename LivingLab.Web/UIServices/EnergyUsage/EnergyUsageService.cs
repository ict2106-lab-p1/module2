using AutoMapper;

using LivingLab.Core.Entities;
using LivingLab.Core.Interfaces.Services.EnergyUsageInterfaces;
using LivingLab.Web.Models.ViewModels.EnergyUsage;

namespace LivingLab.Web.UIServices.EnergyUsage;

/// <remarks>
/// Author: Team P1-1
/// </remarks>
public class EnergyUsageService : IEnergyUsageService
{
    private readonly IMapper _mapper;
    private readonly IEnergyUsageDomainService _energyUsageDomainService;
    
    public EnergyUsageService(IMapper mapper, IEnergyUsageDomainService energyUsageDomainService)
    {
        _mapper = mapper;
        _energyUsageDomainService = energyUsageDomainService;
    }
    
    public Task<List<EnergyUsageViewModel>> GetEnergyUsage(EnergyUsageFilterViewModel filter)
    {
        throw new NotImplementedException();
    }

    public async Task<EnergyBenchmarkViewModel> GetLabEnergyBenchmark(int labId)
    {
        var data = await _energyUsageDomainService.GetLabEnergyBenchmark(labId);
        return _mapper.Map<Lab, EnergyBenchmarkViewModel>(data);
    }

    public Task SetLabEnergyBenchmark(EnergyBenchmarkViewModel benchmark)
    {
        var lab = _mapper.Map<EnergyBenchmarkViewModel, Lab>(benchmark);
        return _energyUsageDomainService.SetLabEnergyBenchmark(lab);
    }
}
