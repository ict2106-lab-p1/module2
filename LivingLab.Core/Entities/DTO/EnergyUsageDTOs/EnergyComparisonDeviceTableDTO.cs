namespace LivingLab.Core.Entities.DTO.EnergyUsageDTOs;

public class EnergyComparisonDeviceTableDTO
{
    public string? DeviceType { get; set; }

    public int TotalEnergyUsage { get; set; }

    public int EnergyUsagePerHour { get; set; }

    public double EnergyUsageCost { get; set; }
}
