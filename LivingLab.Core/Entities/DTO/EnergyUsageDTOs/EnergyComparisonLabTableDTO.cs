namespace LivingLab.Core.Entities.DTO.EnergyUsageDTOs;

public class EnergyComparisonLabTableDTO
{

    public string? LabLocation { get; set; }

    public int TotalEnergyUsage { get; set; }

    public double EnergyUsageCost { get; set; }

    public double EnergyUsageIntensity { get; set; }

    public int EnergyUsagePerHour { get; set; }
}
