using LivingLab.Core.Entities.DTO.EnergyUsageDTOs;
using LivingLab.Web.Models.ViewModels.EnergyUsage;
using LivingLab.Web.Models.ViewModels.LabProfile;

namespace LivingLab.Web.UIServices.EnergyUsage;
/// <remarks>
/// Author: Team P1-2
/// </remarks>
public interface IEnergyUsageAnalysisUIService
{
    public byte[] Export(List<DeviceEnergyUsageDTO> content);
    public List<DeviceEnergyUsageDTO> GetDeviceEnergyUsageByDate(DateTime start, DateTime end);
    public List<LabEnergyUsageDTO> GetLabEnergyUsageByDate(DateTime start, DateTime end);

    // joey
    // trying if shld be DTO or VM
    // DTO
    // public List<TopSevenLabEnergyUsageDTO> GetTopSevenLabEnergyUsage(DateTime start, DateTime end);
    // public List<MonthlyEnergyUsageDTO> GetEnergyUsageTrendAllLab(DateTime start, DateTime end);
    // public List<IndividualLabMonthlyEnergyUsageDTO> GetEnergyUsageTrendSelectedLab(DateTime start, DateTime end, int labId);
    // VM
    public Task<TopSevenLabEnergyUsageDTO> GetTopSevenLabEnergyUsage(DateTime start, DateTime end);
    public Task<EnergyUsageTrendAllLabViewModel> GetEnergyUsageTrendAllLab(EnergyUsageFilterViewModel filter, int labId);
    public Task<EnergyUsageTrendSelectedLabViewModel> GetEnergyUsageTrendSelectedLab(EnergyUsageFilterViewModel filter);
    
    // JOEY ADDED 
    public Task<EnergyBenchmarkViewModel> GetLabEnergyBenchmark(int labId);
    public Task<ViewLabProfileViewModel> GetAllLabs();


    // weijie
    // not sure what will be your DTO looks like may have to create in LivingLab.Core.Entities.DTO.EnergyUsageDTOs;
    public List<DeviceInLabDTO> GetEnergyUsageLabDistribution(DateTime start, DateTime end, string deviceType);
    public List<DeviceInLabDTO> GetEnergyUsageDeviceDistribution(DateTime start, DateTime end, int labID);
}
