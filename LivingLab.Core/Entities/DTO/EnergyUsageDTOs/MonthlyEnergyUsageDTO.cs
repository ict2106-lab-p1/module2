namespace LivingLab.Core.Entities.DTO.EnergyUsageDTOs;
/// <remarks>
/// Author: Team P1-2
/// </remarks>
///
/// JOEY: most likely for EnergyUsageTrendAllLab
public class MonthlyEnergyUsageDTO 
{
    // public string? Month { get; set; }
    //
    // public int TotalEnergyUsage { get; set;}
    
    // following Energy Usage DTO
    public List<EnergyUsageLog> Logs { get; set; }
    public Lab Lab { get; set; }
}
