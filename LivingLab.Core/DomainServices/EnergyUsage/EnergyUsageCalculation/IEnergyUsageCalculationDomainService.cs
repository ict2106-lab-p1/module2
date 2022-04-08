namespace LivingLab.Core.DomainServices.EnergyUsage.EnergyUsageCalculation;
/// <remarks>
/// Author: Team P1-2
/// </remarks>
public interface IEnergyUsageCalculationDomainService
{
    public int CalculateEnergyUsageInWatt(int totalEU, int totalEUTime);
    public double CalculateEnergyUsageCost(double cost, double totalEU);
    public double CalculateEnergyIntensity(int area, int totalEU);

}
