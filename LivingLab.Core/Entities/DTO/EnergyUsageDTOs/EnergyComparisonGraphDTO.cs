namespace LivingLab.Core.Entities.DTO.EnergyUsageDTOs;

public class EnergyComparisonGraphDTO
{
    public string? LabLocation { get; set; }

    public double TotalEnergyUsage { get; set; }

    public double EnergyUsageIntensity { get; set; }

    public double Benchmark { get; set; }
}
