using LivingLab.Core.Interfaces.Services.EnergyUsageInterfaces;
using LivingLab.Core.Entities.DTO.EnergyUsageDTOs;

namespace LivingLab.Core.DomainServices.EnergyUsageServices;
/// <remarks>
/// Author: Team P1-2
/// </remarks>
public class EnergyUsageAnalysisService : IEnergyUsageAnalysisService
{
    public byte[] Export() 
    {
        throw new NotImplementedException();
    }
    public List<DeviceEnergyUsageDTO> GetDeviceEnergyUsageByDate(DateTime start, DateTime end) 
    {
        throw new NotImplementedException();
    }
    public List<LabEnergyUsageDTO> GetLabEnergyUsageByDate(DateTime start, DateTime end) 
    {
        throw new NotImplementedException();
    }
    // joey
    public List<TopSevenLabEnergyUsageDTO> GetTopSevenLabEnergyUsage(DateTime start, DateTime end) 
    {
        throw new NotImplementedException();
    }
    public List<MonthlyEnergyUsageDTO> GetEnergyUsageTrendAllLab(DateTime start, DateTime end) 
    {
        throw new NotImplementedException();
    }
    public List<IndividualLabMonthlyEnergyUsageDTO> GetEnergyUsageTrendSelectedLab(DateTime start, DateTime end, int labId)
    {
        throw new NotImplementedException();
    }
    // weijie
    // not sure what will be your DTO looks like may have to create in LivingLab.Core.Entities.DTO.EnergyUsageDTOs;
    public List<DeviceInLabDTO> GetEnergyUsageLabDistribution(DateTime start, DateTime end, int labId)
    {
        throw new NotImplementedException();
    }
    public List<DeviceInLabDTO> GetEnergyUsageDeviceDistribution(DateTime start, DateTime end, string deviceType)
    {
        throw new NotImplementedException();
    }
}