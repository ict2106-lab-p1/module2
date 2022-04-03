using LivingLab.Core.Entities.DTO.EnergyUsageDTOs;
namespace LivingLab.Core.Interfaces.Services.EnergyUsageInterfaces;
/// <remarks>
/// Author: Team P1-2
/// </remarks>
public interface IEnergyUsageComparisonService 
{
    public List<EnergyComparisonGraphDTO> GetEnergyUsageByLabNameGraph(string labName, DateTime start, DateTime end);
    public List<EnergyComparisonDeviceTableDTO> GetEnergyUsageByDeviceType(string deviceType, DateTime start, DateTime end);
    public List<EnergyComparisonLabTableDTO> GetEnergyUsageByLabNameTable(string labName, DateTime start, DateTime end);

    public double GetEnergyUsageByLabNameBenchmark(string[] labNames, DateTime start, DateTime end); 

    public List<string> GetAllDeviceType();
    public List<string> GetAllLabLocation();
}
