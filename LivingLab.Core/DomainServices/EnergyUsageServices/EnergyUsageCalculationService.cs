
using LivingLab.Core.Entities;
using LivingLab.Core.Interfaces.Services.EnergyUsageInterfaces;
namespace LivingLab.Core.DomainServices.EnergyUsageServices;
/// <remarks>
/// Author: Team P1-2
/// </remarks>
public class EnergyUsageCalculationService : IEnergyUsageCalculationService 
{
    public double CalculateEnergyUsagePerHour(double totalEU, int totalEUTime) 
    {
        throw new NotImplementedException();
    }
    public double CalculateEnergyUsageCost(double cost, double totalEU, double totalEUTime) 
    {
        throw new NotImplementedException();
    }
    public double CalculateEnergyIntensity(int area, int totalEU) 
    {
        throw new NotImplementedException();
    }
    public double CalculateDeviceEUInLab(List<EnergyUsageLog> logs) 
    {
        throw new NotImplementedException();
    }
    public int CalculateBenchMarkForLab(int totalEU, int labCount) 
    {
        throw new NotImplementedException();
    }
    public int CalculateBenchMarkForDeviceType(int totalEU, int deviceCount) 
    {
        throw new NotImplementedException();
    }
    public int CalculateCarbonFootPrint(int totalEU)
    {
        throw new NotImplementedException();
    }
}